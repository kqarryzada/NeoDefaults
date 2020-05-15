using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NeoDefaults_Installer {
    public partial class Main : Form {
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

        // Version value of the program while being developed
        public static readonly String PRODUCT_VERSION = "1.0.0-SNAPSHOT";
#else
        public static readonly bool DEVELOP_MODE = false;

        public static readonly String PRODUCT_VERSION = "1.0.0";
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
         * This is triggered when the "Select Folder" button is clicked.Currently, this makes use of
         * a file-based search and asks the user to provide the 'hl2.exe' file, despite needing a
         * folder.
         * 
         * The .exe is requested despite the fact that the folder is what's really desired. This is
         * because the old-school tree-based folder navigation menu is quite ugly, and it's
         * apparently difficult to configure the more modern menu and have it return a folder (at
         * least, it seemed that way to me). Hence, the program asks for the 'hl2.exe' file in the
         * nicer mavigation menu, and the path will later be modified to record the
         * "Team Fortress 2/tf" folder.
         *
         * Returns a String representing the path to the "Team Fortress 2" folder.
         */
        private String RequestFilepath() {
            String filepath = "";
            bool valid = false;

            using (var fileDialog = new OpenFileDialog()) {
                fileDialog.InitialDirectory = @"C:\";
                fileDialog.Filter = "Program files (*.exe)|*.exe|All files (*.*)|*.*";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK) {
                    // Get the path of specified file
                    filepath = fileDialog.FileName;

                    // Validate the provided filepath
                    String fpCheck = filepath.ToLower();
                    if (fpCheck.Contains("hl2.exe") && fpCheck.Contains("team fortress 2")) {
                        // Clear any previously displayed message
                        buttonPathMessage.ForeColor = Color.DimGray;
                        buttonPathMessage.Text = "";
                        buttonPathMessage.Visible = false;

                        valid = true;
                    }
                    else {
                        // Refuse
                        buttonPathMessage.ForeColor = Color.Red;
                        buttonPathMessage.Text = "Invalid file provided. Please select your 'hl2.exe' file.";
                        buttonPathMessage.Visible = true;
                    }
                }
            }

            // Since the hl2.exe file was given, return the parent directory.
            return (valid) ? Path.GetDirectoryName(filepath) : null;
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
                bool installFonts = true;

                promptInstall.Text = "Installing the Improved Default HUD...";
                String HUDMessage = "";
                var HUDStatus = await utilities.InstallHUD();
                switch (HUDStatus) {
                    case Utilities.InstallStatus.FAIL:
                        HUDMessage = "Error: Failed to install the Improved Default HUD.";
                        break;
                    case Utilities.InstallStatus.SUCCESS:
                        HUDMessage = "Successfully installed the Improved Default HUD.";
                        break;
                    case Utilities.InstallStatus.OPT_OUT:
                        HUDMessage = "HUD installation skipped.";
                        installFonts = false;
                        break;
                    default:
                        log.WriteErr("Received an unexpected return value when trying to install"
                                     + " the HUD: " + HUDStatus);
                        break;
                }
                log.Write("HUD installation returned with status: " + HUDStatus);
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
                            break;
                        case Utilities.InstallStatus.SUCCESS:
                            fontsMessage = "Installed necessary fonts for HUD.";
                            break;
                        default:
                            log.WriteErr("Received an unexpected return value when trying to install"
                                         + " the HUD fonts: " + fontsStatus);
                            break;
                    }
                    log.Write("Font installation returned with status: " + fontsStatus);
                    progressBox.AppendText(fontsMessage + Environment.NewLine);
                }
                else {
                    log.Write("Font installation skipped.");
                }
                progressBar.PerformStep();
            }


            // --- Hitsound --- //
            if (installHitsound) {
                promptInstall.Text = "Installing hitsound files...";
                String hitMessage = "";
                var hitStatus = await utilities.InstallHitsound();
                switch (hitStatus) {
                    case Utilities.InstallStatus.FAIL:
                        hitMessage = "Error: Failed to install the hitsound.";
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
                log.Write("Hitsound installation returned with status: " + hitStatus);
                progressBox.AppendText(hitMessage + Environment.NewLine);
                progressBar.PerformStep();
            }


            // --- Config files --- //
            promptInstall.Text = "Installing config files...";
            String configMessage = "";
            var configStatus = await utilities.InstallConfig();
            switch (configStatus) {
                case Utilities.InstallStatus.FAIL:
                    configMessage = "Error: Failed to install the config files.";
                    break;
                case Utilities.InstallStatus.SUCCESS:
                    configMessage = "Config installed.";
                    break;
                default:
                    log.WriteErr("Received an unexpected return value when trying to install"
                                 + " the config files: " + configStatus);
                    break;
            }
            log.Write("Config installation returned with status: " + configStatus);
            progressBox.AppendText(configMessage + Environment.NewLine);
            progressBar.PerformStep();


            log.Write("Installation complete." + Environment.NewLine);
            promptInstall.Text = "Installation complete.";
            progressBox.AppendText("Installation complete.");
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
                nextPanel = panel4;
            else if (currentPanel == panel4)
                nextPanel = panel5;
            else if (currentPanel == panel5)
                nextPanel = panel6;
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

            String currSavedPath = utilities.tfPath;
            if (currSavedPath != null) {
                // Once the TF2 installation has been found, disallow it from being reset to prevent
                // potential issues.
                SetTFPath(currSavedPath);
                buttonPath.Enabled = false;
                promptPath.Text = "Found the path to the default TF2 install file. Proceed to the" 
                                  + " next page.";
            }
            else {
                promptPath.Text = "Select the location where you installed your game, and find the"
                                  + " \"hl2.exe\" file. This is usually in a location that looks like:\n\r"
                                  + @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2\hl2.exe";
            }

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
            if (filepath != null) {
                filepath = utilities.CanonicalizePath(Path.Combine(filepath, "tf"));

                // Attempt setting the filepath
                if (SetTFPath(filepath))
                    return;
            }

            nextPath.Enabled = false;
        }

        /**
         * Records the user's wish to opt into installing idHUD.
         */
        private void menu1Hitsound_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHitsound = true;
            }
        }

        /**
         * Records the user's wish to opt out of installing idHUD.
         */
        private void menu1HUD_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHUD = true;
            }
        }

        /**
         * Records the user's wish to opt into installing the custom hitsound.
         */
        private void menu2Hitsound_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHitsound = false;
            }
        }

        /**
         * Records the user's wish to opt out of installing the custom hitsound.
         */
        private void menu2HUD_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHUD = false;
            }
        }
    }
}
