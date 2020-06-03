﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NeoDefaults_Installer {
    public partial class Main : Form {
        public static readonly String PRODUCT_VERSION = "1.0.0-SNAPSHOT";

        // Defines the default size of the main screen.
        private readonly Size DEFAULT_WINDOW_SIZE = new Size(640, 480);

        // The approximate number of components expected to be installed. This includes elements
        // like the autoexec file, the HUD, etc. This is used to determine what percentage of the
        // progress bar should be filled each time a component finishes installing.
        private readonly int NUM_COMPONENTS = 4;

        // A reference to the panel that is currently being displayed on the main screen.
        private Panel currentPanel;

        // Specifies whether the user requested additional customizations.
        private bool installHUD = true;
        private bool installHitsound = true;

        // Specifies the installation type.
        private bool isBasicInstallEnabled = true;

        // Keeps track of which installations failed so that they may be reported at the end.
        private readonly List<String> failedComponents = new List<String>();

        // Keeps track of which previous menus were accessed, so that they can be made available if
        // someone clicks "Back".
        private Stack<Panel> stack = new Stack<Panel>();

        // Stores useful utility methods
        Utilities utilities;

        // Logfile containing useful information on what the program has been doing
        Logger log;

#if DEBUG
        // Store the DEBUG flag as a member variable to allow using its value in if statements.
        public static readonly bool DEVELOP_MODE = true;
#else
        public static readonly bool DEVELOP_MODE = false;
#endif


        public Main() {
            InitializeComponent();

            // Append the version to the title bar of the application window
            this.Text += PRODUCT_VERSION;

            utilities = Utilities.GetInstance();
            log = Logger.GetInstance();

            // Set the current panel to the home panel, and restrict the window size to hide
            // other panels
            currentPanel = panel1;
            this.Size = DEFAULT_WINDOW_SIZE;
            this.MaximizeBox = false;
        }


        /**
         * Launches a dialog box and asks the user to provide the path to their TF2 install.
         *
         * This is triggered when the "Select Folder" button is clicked. Currently, this makes use
         * of a file-based search and asks the user to provide the 'hl2.exe' file, despite needing a
         * folder.
         *
         * Returns a String representing the path to the "Team Fortress 2/tf" folder. If the
         * operation was cancelled or invalid, the String will be null.
         */
        private String RequestFilepath() {
            String hl2Path = "";
            bool valid = false;

            // The .exe is requested despite the fact that a folder is what's really desired. This
            // is because the old-school tree-based folder navigation menu is quite ugly, and it's
            // apparently difficult to configure the more modern menu and have it return a folder
            // (at least, it seemed that way to me). Hence, the program asks for the 'hl2.exe' file
            // in the nicer mavigation menu, and the path will be modified to record the
            // "Team Fortress 2/tf" folder.
            using (var fileDialog = new OpenFileDialog()) {
                fileDialog.InitialDirectory = @"C:\";
                fileDialog.Filter = "Program files (*.exe)|*.exe|All files (*.*)|*.*";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK) {
                    // Get the path of specified file
                    hl2Path = fileDialog.FileName;
                    log.Write("Attempting to set the user-given path: " + hl2Path);

                    // Validate the provided filepath
                    String fpCheck = hl2Path.ToLower();
                    if (fpCheck.Contains("hl2.exe") && fpCheck.Contains("team fortress 2")) {
                        // Clear any previously-displayed message
                        buttonPathMessage.ForeColor = Color.DimGray;
                        buttonPathMessage.Text = "";
                        buttonPathMessage.Visible = false;

                        valid = true;
                    }
                    else {
                        buttonPathMessage.ForeColor = Color.Red;
                        buttonPathMessage.Text = "Invalid file provided. Please select your 'hl2.exe' file.";
                        buttonPathMessage.Visible = true;
                    }
                }
            }

            String retval = null;
            if (valid) {
                log.WriteLn("The path was considered valid.");
                try {
                    // Modify the path to return the tf/ folder.
                    String parent = Path.GetDirectoryName(hl2Path);
                    retval = Path.Combine(parent, "tf");
                }
                catch (Exception e) {
                    log.WriteErr("Failed to obtain the path to tf/ from: " + hl2Path,
                                    e.ToString());
                }
            }
            else {
                log.WriteLn("The path was considered invalid.");
            }

            return retval;
        }


        /**
         * Writes information about the program state to the log file.
         */
        private void LogStatus() {
            List<String> msg = new List<String>();
            msg.Add("");
            msg.Add("Status of the program:");
            msg.Add("Develop mode is: " + DEVELOP_MODE);
            msg.Add("Basic install flag is: " + isBasicInstallEnabled);
            msg.Add("HUD install is: " + installHUD);
            msg.Add("Hitsound install is: " + installHitsound);
            msg.Add("The TF Path is: " + utilities.tfPath);
            msg.Add("");

            log.Write(msg.ToArray());
        }

        /**
         * Sets all the necessary parameters to initialize the progress bar on the UI.
         */
        private void InitializeProgressbar() {
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Step = 1;

            // Adjust number of components to be moved based on user choice. If they have opted out
            // of a component, that's one less component that will be installed.
            progressBar.Maximum = NUM_COMPONENTS;
            progressBar.Maximum -= (installHUD) ? 0 : 2;
            progressBar.Maximum -= (installHitsound) ? 0 : 1;

            progressBar.Visible = true;
        }


        /**
         * Installs the requested files to the appropriate locations. These operations are done on
         * their own threads to allow the UI to be responsive while the user waits for the
         * installation to complete.
         */
        private async void InstallFiles() {
            // Report the state of program to the log file before the install begins.
            LogStatus();

            // Initialize before starting the installation
            InitializeProgressbar();

            if (installHUD) {
                // --- HUD --- //
                bool installFonts = false;

                promptInstall.Text = "Installing the Improved Default HUD...";
                String HUDMessage = "";
                var HUDStatus = await utilities.InstallHUD();
                switch (HUDStatus) {
                    case Utilities.InstallStatus.FAIL:
                        HUDMessage = "Error: Failed to install the Improved Default HUD.";
                        failedComponents.Add("Improved Default HUD");
                        break;
                    case Utilities.InstallStatus.SUCCESS:
                        HUDMessage = "Successfully installed the Improved Default HUD.";
                        installFonts = true;
                        break;
                    case Utilities.InstallStatus.OPT_OUT:
                        HUDMessage = "HUD installation skipped.";
                        break;
                    default:
                        log.WriteErr("Received an unexpected return value when trying to install"
                                     + " the HUD: " + HUDStatus);
                        break;
                }
                log.WriteLn("HUD installation returned with status: " + HUDStatus);
                progressBox.AppendText(HUDMessage + Environment.NewLine);
                progressBar.PerformStep();


                // --- Fonts --- //
                // If the user opts out of installing the HUD, then skip installing the fonts.
                if (installFonts) {
                    log.Write("Installing font files...");

                    promptInstall.Text = "Installing required fonts for the Improved Default HUD...";
                    String fontsMessage = "";
                    var fontsStatus = await utilities.InstallHUDFonts();
                    switch (fontsStatus) {
                        case Utilities.InstallStatus.FAIL:
                            fontsMessage = "Error: Failed to install the needed fonts for the HUD.";
                            failedComponents.Add("Fonts for the Improved Default HUD");
                            break;
                        case Utilities.InstallStatus.SUCCESS:
                            fontsMessage = "Installed necessary fonts for HUD.";
                            break;
                        case Utilities.InstallStatus.ALREADY_INSTALLED:
                            fontsMessage = "All HUD font files are already installed.";
                            break;
                        default:
                            log.WriteErr("Received an unexpected return value when trying to install"
                                         + " the HUD fonts: " + fontsStatus);
                            break;
                    }
                    log.WriteLn("Font installation returned with status: " + fontsStatus);
                    progressBox.AppendText(fontsMessage + Environment.NewLine);
                }
                else {
                    log.Write("Font installation skipped.");
                }
                progressBar.PerformStep();
            }
            else {
                progressBox.AppendText("Opted out of the HUD installation. Skipping."
                                       + Environment.NewLine);
            }


            // --- Hitsound --- //
            if (installHitsound) {
                promptInstall.Text = "Installing hitsound files...";
                String hitMessage = "";
                var hitStatus = await utilities.InstallHitsound();
                switch (hitStatus) {
                    case Utilities.InstallStatus.FAIL:
                        hitMessage = "Error: Failed to install the hitsound.";
                        failedComponents.Add("Hitsound");
                        break;
                    case Utilities.InstallStatus.SUCCESS:
                        hitMessage = "Hitsound installed.";
                        break;
                    case Utilities.InstallStatus.OPT_OUT:
                        hitMessage = "Hitsound install skipped.";
                        break;
                    default:
                        log.WriteErr("Received an unexpected return value when trying to install"
                                     + " the hitsound: " + hitStatus);
                        break;
                }
                log.WriteLn("Hitsound installation returned with status: " + hitStatus);
                progressBox.AppendText(hitMessage + Environment.NewLine);
                progressBar.PerformStep();
            }
            else {
                progressBox.AppendText("Opted out of the hitsound installation. Skipping."
                                       + Environment.NewLine);
            }


            // --- Config files --- //
            promptInstall.Text = "Installing config files...";
            String configMessage = "";
            var configStatus = await utilities.InstallConfig();
            switch (configStatus) {
                case Utilities.InstallStatus.FAIL:
                    configMessage = "Error: Failed to install the config files.";
                    failedComponents.Add("Config");
                    break;
                case Utilities.InstallStatus.SUCCESS:
                    configMessage = "Config installed.";
                    break;
                default:
                    log.WriteErr("Received an unexpected return value when trying to install"
                                 + " the config files: " + configStatus);
                    break;
            }
            log.WriteLn("Config installation returned with status: " + configStatus);
            progressBox.AppendText(configMessage + Environment.NewLine);
            progressBar.PerformStep();

            String completeMessage = "Installation complete.";
            if (failedComponents.Any())
                completeMessage += " There was a problem with one or more components.";
            log.Write(completeMessage);
            promptInstall.Text = completeMessage;
            // Allow user to continue to the next page
            nextInstall.Enabled = true;
        }


        /**
         * Saves the location of the user's TF2 install. The full canonical path of 'path' is
         * computed by this method.
         *
         * Returns a boolean indicating whether or not the attempt was successful.
         */
        public bool SetTFPath(String path) {
            path = utilities.CanonicalizePath(path);
            if (path == null) {
                return false;
            }

            // Update button info to indicate where the install files were found
            buttonPathMessage.Text = path;
            buttonPathMessage.Visible = true;

            utilities.tfPath = path;

            // Allow user to proceed to next page
            nextPath.Enabled = true;
            return true;
        }


        /**
         * Prepares the final page with a "success" message if there were no failures. Otherwise,
         * the component failures are listed on this page.
         */
        private void PrepareFarewellPage() {
            String message;
            Image image;
            if (!failedComponents.Any()) {
                image = SystemIcons.Information.ToBitmap();
                message = "Congrations! You done it. All files successfully installed.";
            }
            else {
                image = SystemIcons.Error.ToBitmap();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("The following components failed to install:");
                sb.AppendLine();
                foreach (String component in failedComponents) {
                    sb.Append("• ");
                    sb.AppendLine(component);
                }

                message = sb.ToString();
            }

            promptLast.Text = message;
            imageLast.Image = image;
        }


        // --------------------------------- //
        //       Panel-specific methods      //
        // --------------------------------- //

        /**
         * Navigates to the previous page in the setup menu.
         */
        private void PreviousPanel() {
            try {
                UpdateScreen(stack.Pop());
            }
            catch (InvalidOperationException ioe) {
                log.WriteErr("An error occurred when trying to pop the panel stack:",
                             ioe.ToString());
                var dialog = new ErrorDialog();
                dialog.DisplayAndExit("Tried to move to the previous screen, but an unexpected"
                                      + " error occurred. The program will now exit.");
            }
        }

        /**
         * Navigates to the next page in the setup menu.
         */
        private void NextPanel() {
            // Save current panel in case the user later wishes to go back.
            stack.Push(currentPanel);

            Panel nextPanel;
            if (currentPanel == panel1) {
                // Prepare elements on the next page before displaying anything.
                PreparePathPanel();
                nextPanel = panel2;
            }
            else if (currentPanel == panel2) {
                if (!isBasicInstallEnabled)
                    nextPanel = panel3;
                else
                    nextPanel = panel5;
            }
            else if (currentPanel == panel3)
                nextPanel = panel5;
            else if (currentPanel == panel5) {
                PrepareFarewellPage();
                nextPanel = panel6;
            }
            else if (currentPanel == panel6) {
                // The button on the last screen exits.
                Environment.Exit(0);
                return;
            }
            else {
                String panelName = (currentPanel == null) ? "<null>" : currentPanel.Name;
                // It should not be possible to get an unknown panel onto the stack.
                log.WriteErr("SEVERE ERROR: The current panel is currently set to '"
                             + panelName + "', which is not defined.");
                var dialog = new ErrorDialog();
                dialog.DisplayAndExit("An error occurred when trying to go to the next page."
                                      + " The program will now exit.");
                return;
            }
            UpdateScreen(nextPanel);
            
            // Certain panels need to execute tasks immediately upon switching to the next page.
            if (currentPanel == panel5) {
                InstallFiles();
            }
        }

        /**
         * Updates the UI to show the new panel.
         */
        private void UpdateScreen(Panel newPanel) {
            Panel oldPanel = currentPanel;

            // Swap the two panels' locations. Since the main window is (almost) the size of one
            // panel, this will effectively make it appear that the new panel is just the next page.
            oldPanel.Location = newPanel.Location;
            newPanel.Location = new Point(0, 0);

            currentPanel = newPanel;
        }

        /**
         * When navigating to the path page (the page that asks the user to enter their TF path),
         * the program needs to load elements based on whether or not a TF2 install has been found.
         * To avoid issues when navigating between pages, this method will only make changes to the
         * UI once.
         */
        private bool visited = false;
        private void PreparePathPanel() {
            if (visited)
                return;

            String displayMessage;
            String currSavedPath = utilities.tfPath;
            if (currSavedPath != null) {
                SetTFPath(currSavedPath);
                displayMessage = "Found the path to the default TF2 install file. Proceed to the"
                                 + " next page.";
            }
            else {
                displayMessage = "Select the location where you installed your game, and find the"
                                 + " \"hl2.exe\" file. This is usually in a location that looks like:\n\r"
                                 + @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2\hl2.exe";
            }
            promptPath.Text = displayMessage;
            visited = true;
        }


        // --------------------------------- //
        //       Event-handling methods      //
        // --------------------------------- //

        // The following methods handle an action when an event is triggered on the UI.

        /**
         * General method for navigating to the previous page in the setup menu.
         */
        private void Prev_Click(object sender, EventArgs e) {
            PreviousPanel();
        }

        /**
         * General method for navigating to the next page in the setup menu.
         */
        private void Next_Click(object sender, EventArgs e) {
            NextPanel();
        }

        /**
         * Configures setup to execute the basic install.
         */
        private void basic_install_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                isBasicInstallEnabled = true;
            }
        }

        /**
         * Configures setup to execute the advanced install.
         */
        private void advanced_install_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                isBasicInstallEnabled = false;
            }
        }

        /**
         * This event is triggered when the user clicks "Select Folder" to provide the path to their
         * TF2 installation.
         */
        private void buttonPath_Click(object sender, EventArgs e) {
            String filepath = RequestFilepath();
            bool success = false;
            if (filepath != null) {
                log.Write("Attempting to set the filepath to: " + filepath);
                success = SetTFPath(filepath);
            }

            if (!success)
                nextPath.Enabled = false;
        }

        /**
         * Records the user's wish to opt into or out of installing the custom hitsound.
         */
        private void HitsoundBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox element = (CheckBox) sender;
            installHitsound = element.Checked;
        }

        /**
         * Records the user's wish to opt into or out of installing idHUD.
         */
        private void HUDBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox element = (CheckBox) sender;
            installHUD = element.Checked;
        }
    }
}
