using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NeoDefaults_Installer.warning_dialog;
using InstallStatus = NeoDefaults_Installer.Utilities.InstallStatus;

namespace NeoDefaults_Installer {
    public partial class Main : Form {
        public static readonly String PRODUCT_VERSION = "1.0.1-SNAPSHOT";

        // Defines the default size of the main screen.
        private readonly Size DEFAULT_WINDOW_SIZE = new Size(640, 480);

        // A reference to the panel that is currently being displayed on the main screen.
        private Panel currentPanel;

        private readonly ComponentsManager ComponentsMgr = new ComponentsManager();

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

        private static readonly String INSTALL_NOT_FOUND =
                                "Open the location where you installed your game, and select the"
                                + " file named \"hl2\" or \"hl2.exe\". This is usually in a folder"
                                + " that looks like:"
                                + Environment.NewLine
                                + @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2\";

        // Tracks whether the user has ever successfully provided the path to the TF2 install themselves.
        private bool folderManuallySelected = false;


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
         * Launches a dialog box and asks the user to provide the path to the 'hl2.exe' file inside
         * their TF2 installation.
         *
         * This method should be called when the "Select Folder" button is clicked. It makes use of
         * a file-based search and asks the user to provide 'hl2.exe' in the TF2 install folder.
         * This method does not perform validation on the filepath that is returned, aside from
         * checking that the file has the correct name.
         *
         * If the user provided a file that is not named 'hl2.exe', a null String will be returned.
         * If the user cancelled the operation and did not provide a file, the String "cancel"
         * will be returned.
         * Otherwise, the return String will represent a path to a 'tf' directory that will need to
         * be validated.
         */
        private String RequestTF2Filepath() {
            String defaultDirectory = @"C:\";
            try {
                defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            }
            catch (Exception) {
                log.Write("Failed to obtain the path to ProgramFiles(x86) for folder navigation.");
            }

            // The .exe is requested despite the fact that a folder is what's really desired. This
            // is because the old-school tree-based folder navigation menu is quite ugly, and it's
            // apparently difficult to configure the more modern menu and have it return a folder
            // (at least, it seemed that way to me). Hence, the program asks for the 'hl2.exe' file
            // in the nicer mavigation menu, and the path will be modified to record the
            // "Team Fortress 2/tf" folder.
            String hl2Path;
            using (var fileDialog = new OpenFileDialog()) {
                fileDialog.InitialDirectory = defaultDirectory;
                fileDialog.Filter = "Program files (*.exe)|*.exe|All files (*.*)|*.*";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() != DialogResult.OK) {
                    // The operation was cancelled
                    return "cancel";
                }

                hl2Path = fileDialog.FileName;
            }

            log.Write("Considering the user-given path: " + hl2Path);
            if (!Utilities.ValidateHL2Exe(hl2Path)) {
                log.Write("The name of the file was invalid.");
                return null;
            }
            log.WriteLn("The path contains a valid .exe file.");


            String retval = null;
            try {
                // Modify the path to return the tf/ folder.
                String parent = Path.GetDirectoryName(hl2Path);
                retval = Path.Combine(parent, "tf");
            }
            catch (Exception e) {
                log.WriteErr("Failed to obtain the path to tf/ from: " + hl2Path,
                                e.ToString());
            }

            return retval;
        }


        /**
         * Writes information about the program state to the log file.
         */
        private void ReportCurrentStatus() {
            List<String> msg = new List<String>();
            msg.Add("");
            msg.Add("Status of the program:");
            msg.Add("Develop mode is: " + DEVELOP_MODE);
            msg.Add("Basic install flag is: " + isBasicInstallEnabled);
            msg.Add("HUD install is: " + ComponentsMgr.HUDInstallEnabled());
            msg.Add("Hitsound install is: " + ComponentsMgr.HitsoundInstallEnabled());
            msg.Add("Config install is: " + ComponentsMgr.ConfigInstallEnabled());
            msg.Add("The TF Path is: " + utilities.tfPath);
            msg.Add("");

            log.Write(msg.ToArray());
        }

        /**
         * Sets all the necessary parameters to initialize the progress bar on the UI.
         */
        private void InitializeProgressbar() {
            progressBar.Minimum = 0;
            progressBar.Maximum = ComponentsMgr.NumberOfEnabledComponents();
            progressBar.Value = 0;
            progressBar.Step = 1;
            progressBar.Visible = true;
        }


        /**
         * Logs the status of an attempted component install, and updates the progress box to
         * indicate the status to the user in a nicer message.
         *
         * longName: The full name of the installed component
         * shortName: The nickname of the installed component
         * status: The result code of the attempted install
         */
        private void LogInstallStatus(String longName, String shortName, InstallStatus status) {
            log.WriteLn("The " + shortName + " installation returned with status: " + status);

            String message = "";
            switch (status) {
                case InstallStatus.FAIL:
                    message = "Error: Failed to install the " + longName + ".";
                    failedComponents.Add(longName);
                    break;
                case InstallStatus.SUCCESS:
                    message = "Installed the " + longName + ".";
                    break;
                default:
                    log.WriteErr("Received an unexpected return value when trying to install"
                                 + " the " + longName + ": " + status);
                    break;
            }

            progressBox.AppendText(message + Environment.NewLine);
        }


        /**
         * Installs the requested files to the appropriate locations. These operations are done on
         * their own threads to allow the UI to be responsive while the user waits for the
         * installation to complete.
         */
        private async void InstallFiles() {
            // Report the state of program to the log file before the install begins.
            ReportCurrentStatus();

            // Initialize before starting the installation
            InitializeProgressbar();


            // --- Config files --- //
            if (ComponentsMgr.ConfigInstallEnabled()) {
                String configComponentName = "NeoDefaults config files";
                promptInstall.Text = "Installing " + configComponentName + "...";
                var configStatus = await utilities.InstallConfig();
                LogInstallStatus(configComponentName, "config", configStatus);
                progressBar.PerformStep();
            }
            else {
                progressBox.AppendText("Opted out of the installation for the config files. Skipping."
                                       + Environment.NewLine);
            }


            // --- HUD --- //
            if (ComponentsMgr.HUDInstallEnabled()) {
                String HUDComponentName = "NeoDefaults HUD tweaks (damage numbers)";
                promptInstall.Text = "Installing the " + HUDComponentName + "...";
                var HUDStatus = await utilities.InstallHUD();
                LogInstallStatus(HUDComponentName, "HUD", HUDStatus);
                progressBar.PerformStep();
            }
            else {
                progressBox.AppendText("Opted out of the HUD installation. Skipping."
                                       + Environment.NewLine);
            }


            // --- Hitsound --- //
            if (ComponentsMgr.HitsoundInstallEnabled()) {
                String hitsoundComponentName = "hitsound files";
                promptInstall.Text = "Installing the " + hitsoundComponentName + "...";
                var hitStatus = await utilities.InstallHitsound();
                LogInstallStatus(hitsoundComponentName, "hitsound", hitStatus);
                progressBar.PerformStep();
            }
            else {
                progressBox.AppendText("Opted out of the hitsound installation. Skipping."
                                       + Environment.NewLine);
            }

            String completeMessage = "Installation complete.";
            if (failedComponents.Any())
                completeMessage += " There was a problem with one or more components.";
            log.Write(completeMessage);
            promptInstall.Text = completeMessage;
            // Allow user to continue to the next page
            nextInstall.Enabled = true;
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
                sb.Append("The following components failed to install.");
                sb.AppendLine(" Visit the \"More Info\" page on GitHub for advice on troubleshooting.");
                sb.AppendLine();
                foreach (String component in failedComponents) {
                    String capitalized = component.First().ToString().ToUpper() + component.Substring(1);
                    sb.Append("• ");
                    sb.AppendLine(capitalized);
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
                var previousPanel = stack.Pop();
                UpdateScreen(previousPanel);
                log.Write("<--- Navigated back to '" + previousPanel?.Name + "'.");
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
            log.Write("---> Navigated forward to '" + nextPanel?.Name + "'.");

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
         */
        private void PreparePathPanel() {
            // Basic installs will immediately move to the installation page
            nextPath.Text = (isBasicInstallEnabled) ? "Install" : "Next";

            String displayMessage;
            if (utilities.tfPath != null) {
                buttonPathMessage.Text = utilities.tfPath;
                nextPath.Enabled = true;

                // The success message depends on whether the user provided the path manually, or if
                // it was found by Utilities.SearchForTF2Install.
                if (folderManuallySelected)
                    displayMessage = "The TF2 install path was recognized. Proceed to the next page.";
                else
                    displayMessage = "The TF2 install path was automatically found. Proceed to the next page.";
            }
            else {
                // Clear any previously-displayed error messages upon arriving at this panel.
                buttonPathMessage.Text = "";
                nextPath.Enabled = false;
                displayMessage = INSTALL_NOT_FOUND;
            }
            promptPath.Text = displayMessage;

            // Reset the color, in case an error was previously shown
            buttonPathMessage.ForeColor = Color.DimGray;
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
        private void ButtonPath_Click(object sender, EventArgs e) {
            String filepath = RequestTF2Filepath();
            if (filepath != null && filepath.Equals("cancel")) {
                // If the operation was cancelled, leave immediately without changing the state of
                // the program.
                return;
            }

            bool success = false;
            try {
                success = utilities.SetTFPath(filepath);
            }
            catch (Exception ex) {
                var dialog = new WarningDialog();
                dialog.Display("A problem occurred while trying to check that the file was valid."
                                + "The error message was: "
                                + Environment.NewLine
                                + Environment.NewLine
                                + ex.ToString());
            }
            if (success) {
                folderManuallySelected = true;

                nextPath.Enabled = true;
                buttonPathMessage.ForeColor = Color.DimGray;
                buttonPathMessage.Text = utilities.tfPath;
                promptPath.Text = "The TF2 install path was recognized. Proceed to the next page.";
            }
            else {
                nextPath.Enabled = false;
                buttonPathMessage.ForeColor = Color.Red;
                buttonPathMessage.Text = "Invalid file provided. Please select your \"hl2\" file.";
                promptPath.Text = INSTALL_NOT_FOUND;
            }
            buttonPathMessage.Visible = true;
        }

        /**
         * Records the user's wish to opt into or out of installing the custom hitsound.
         */
        private void HitsoundBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox element = (CheckBox) sender;
            ComponentsMgr.SetHitsound(element.Checked);
            CheckInstallAllowed();
        }

        /**
         * Records the user's wish to opt into or out of installing the HUD VPK.
         */
        private void HUDBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox element = (CheckBox) sender;
            ComponentsMgr.SetHUD(element.Checked);
            CheckInstallAllowed();
        }

        /**
         * Records the user's wish to opt into or out of installing the config.
         */
        private void CfgBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox element = (CheckBox) sender;
            ComponentsMgr.SetConfig(element.Checked);
            CheckInstallAllowed();
        }

        /**
         * This method is called every time a component is selected or de-selected on the Advanced
         * Installation menu. If all components are de-selected, then the installation should
         * refuse to run.
         */
        private void CheckInstallAllowed() {
            NextOpt.Enabled = !(ComponentsMgr.NumberOfEnabledComponents() == 0);
        }
    }
}
