using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace NeoDefaults_Installer {
    public partial class Main : Form {
        public static readonly bool DEVELOP_MODE = true;

        // Defines the default size of the main screen.
        private readonly Size DEFAULT_WINDOW_SIZE = new Size(640, 480);

        // The approximate number of components expected to be installed. This includes elements
        // like the autoexec file, the HUD, etc.
        private readonly int NUM_COMPONENTS = 4;

        // A reference to the panel object that is currently being displayed on the main screen.
        private Panel currentPanel;

        // Specifies whether user requested additional customizations.
        private bool installHUD = true;
        private bool installHitsound = true;

        // Specifies install type
        private bool isBasicInstallEnabled = true;

        // Keeps track of which previous menus were accessed, so that they can be made available if
        // someone clicks "Back".
        private Stack<Panel> stack = new Stack<Panel>();

        // Stores useful utility methods
        Utilities utilities;

        // Logfile containing useful information on what the program has been doing
        Logger log;


        public Main() {
            InitializeComponent();
            utilities = Utilities.GetInstance();
            log = Logger.GetInstance();

            // Set the current panel to the home panel, and restrict the window size to hide
            // other panels
            currentPanel = panel1;
            this.Size = DEFAULT_WINDOW_SIZE;
            this.MaximizeBox = false;
        }

        /**
         * When the "Select Folder" button is clicked, launch a menu to allow the user to specify
         * the location of their install. Currently, this makes use of a file-based search and asks
         * the user to provide the 'hl2.exe' file, despite needing a folder.
         * 
         * This was done since the typical tree-based folder navigation menu is quite ugly, and
         * it's apparently difficult to successfully use the more modern menu (which is used here)
         * but return a folder instead (at least, it seemed that way to me). Hence, the program asks
         * for the 'hl2.exe' file in the nicer mavigation menu, and strips off the filename to
         * record the "Team Fortress 2" folder.
         *
         * Returns a String representing the filepath the user selected.
         */
        private String RequestFilepath() {
            String filepath = "";
            bool valid = true;

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Filter = "Program files (*.exe)|*.exe|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    // Get the path of specified file
                    filepath = openFileDialog.FileName;

                    // Validate the filepath
                    String fpCheck = filepath.ToLower();
                    if (!fpCheck.Contains("hl2.exe") || !fpCheck.Contains("team fortress 2")) {
                        // Refuse
                        buttonPathMessage.ForeColor = Color.Red;
                        buttonPathMessage.Text = "Invalid file provided.";
                        valid = false;
                    }
                }
            }

            // Since hl2.exe is selected, point 'filePath' at the parent directory if it is not null.
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
                promptInstall.Text = "Installing the Improved Default HUD...";
                await utilities.InstallHUD();
                progressBar.PerformStep();

                promptInstall.Text = "Installing required fonts for the Improved Default HUD...";
                await utilities.InstallHUDFonts();
                progressBar.PerformStep();
            }

            if (installHitsound) {
                promptInstall.Text = "Installing hitsound files...";
                await utilities.InstallHitsound();
                progressBar.PerformStep();
            }

            // Install autoexec.cfg file
            promptInstall.Text = "Installing config files...";
            await utilities.InstallConfig();
            progressBar.PerformStep();

            Thread.Sleep(100);
            promptInstall.Text = "Installation complete.";
            // Progress complete. Allow user to continue to next page
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

            // Save the path, as it will be needed later
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
            catch (InvalidOperationException) {
                log.Write("Tried to move to previous screen, but an unexpected error"
                                + " occurred. The most likely cause is that the stack was popped"
                                + " when it was empty, but this is not expected behavior.");
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
                log.Write("SEVERE ERROR: The current panel is currently set to '" 
                          + panelName + "', which is not defined.", 
                          "This is not expected behavior.");
                Environment.Exit(1);
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
         * This method will only make changes to the UI once.
         */
        private bool first = true;
        private void PreparePathPanel() {
            // If the path to the TF2 install was found during startup, disable the ability for
            // the user to reset it. This is done only once to prevent strange issues when switching
            // between panels.
            if (!first)
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
                                  + " \"hl2.exe\" file.\n\r This is usually in a location that looks like:\n\r"
                                  + @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2\hl2.exe";
            }

            first = false;
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
                filepath = utilities.CanonicalizePath(filepath);

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
