using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO.Compression;
using System.Threading;

namespace CfgInstallerPrototype {
    public partial class Main : Form {
        private readonly bool DEVELOP_MODE = true;

        // Defines the default size of the main screen.
        private readonly Size DEFAULT_WINDOW_SIZE = new Size(640, 480);

        // The autoexec file to copy 
        private readonly String autoexecSourceName = "custom-files/autoexec-alpha.cfg";
        private readonly String autoexecDestName = "autoexec-TEST.cfg";

        // TF-path related parameters
        private readonly String DEFAULT_TF2_PATH = @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2";
        //private readonly String DEFAULT_TF2_PATH = @"E:\Steam\SteamApps\common\Team Fortress 2";
        private readonly String basePath;
        private String tfPath;

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

        public Main() {
            InitializeComponent();

            // Set the current panel to the home panel, and restrict the window size to hide
            // other panels
            currentPanel = panel1;
            this.Size = DEFAULT_WINDOW_SIZE;

            // Disable ability to maximize window
            this.MaximizeBox = false;

            // When developing, the base filepath is four parent directories above the
            // executable. When the .exe is packaged, it's just the current parent directory.
            String parentPath = (DEVELOP_MODE) ? @"..\..\..\.." : @"..";
            
            String relativeBasePath = AppDomain.CurrentDomain.BaseDirectory;
            relativeBasePath = Path.Combine(relativeBasePath, parentPath);
            basePath = new FileInfo(relativeBasePath).FullName;

            // On startup, try to determine the path to the TF2 installation.
            checkDefaultTF2Install();
        }

        /**
         * When "Advanced install" is selected, clear the appropriate flag.
         */
        private void advanced_install_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                isBasicInstallEnabled = false;
            }
        }

        /**
         * When "Basic install" is selected, set the appropriate flag.
         */
        private void basic_install_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                isBasicInstallEnabled = true;
            }
        }

        private void prompt_Click(object sender, EventArgs e) {

        }

        /**
         * Tries to find a TF2 installation in most common location. 
         * 
         * Returns true if the install files were found.
         */
        private bool checkDefaultTF2Install() {
            // Check to see if the "hl2.exe" binary is in the default location.
            if (File.Exists(Path.Combine(DEFAULT_TF2_PATH, "hl2.exe"))) {
                tfPath = DEFAULT_TF2_PATH;

                // Before leaving, enable the button on the "path" page to let the user proceed
                nextPath.Enabled = true;
                return true;
            }

            return false;
        }

        /**
         * When the "Select Folder" button is clicked, launch a menu to allow the user to specify
         * the location of their install. Currently, this makes use of a file-based search and asks
         * the user to provide the 'hl2.exe' file, despite needing a folder.
         * 
         * This was done since the typical tree-based folder navigation menu is not very good, and
         * it's apparently difficult to successfully use the more modern menu (which is used here)
         * but return a folder instead (at least, it seemed that way to me). Hence, the program asks
         * for the 'hl2.exe' file in the nicer mavigation menu, and strips off the filename to
         * record the "Team Fortress 2" folder.
         */
        private void buttonPath_Click(object sender, EventArgs e) {
            String filePath = "";
            bool valid = true;

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Program files (*.exe)|*.exe|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    // Get the path of specified file
                    filePath = openFileDialog.FileName;

                    // Validate the filepath
                    String fpCheck = filePath.ToLower();
                    if (!fpCheck.Contains("hl2.exe") || !fpCheck.Contains("team fortress 2")) {
                        // Refuse
                        return;
                    }
                    if (!fpCheck.Contains("steamapps")) {
                        // Display warning
                        valid = false;
                    }
                }
            }

            if (valid) {
                // Since hl2.exe is selected, point 'filePath' at the parent directory
                filePath = Path.Combine(filePath, "..");
                tfPath = new FileInfo(filePath).FullName;
                nextPath.Enabled = true;
                buttonPathMessage.Text = tfPath;
                buttonPathMessage.Visible = true;
            }
            else {
                nextPath.Enabled = false;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {

        }


        private void copyFile(String sourceFile, String destFile) {
            copyFile(sourceFile, destFile, false);
        }

        private readonly String backupFolder = @"";

        /**
         * Copies a 'sourceFile' to a destination. The filepath of the destination is given by 
         * 'destFile', which includes the filename of the file (allowing for a move-and-rename
         * operation to be executed at once). This method will give a few attempts at copying the file
         * over before reporting an issue.
         */
        private void copyFile(String sourceFile, String destFile, bool overwrite) {
            int NUM_RETRIES = 3;

            // If 'destFile' already exists before the copy process, notify the user and ask if they
            // want to continue.
            if (!overwrite && File.Exists(destFile)) {
                // Form dlg1 = new Form();
                // dlg1.ShowDialog();
                Debug.Print("The destination file '" + destFile + "' already exists.");

                // pseudo-code:
                // switch (response) {
                //     case overwrite_ok:
                //         overwrite = true;
                //         break;
                //     case skip:
                //         // The file is no longer needed to be copied over, so leave.
                //         return;
                //     case backup:
                //         // do backup
                //          break;
                // }
            }

            // Allow a few retry attempts in case of transient issues.
            for (int i = 0; i < NUM_RETRIES; i++) {
                try {
                    // Attempt a copy. If it is successful, leave the loop. Otherwise, the exception
                    // is caught.
                    File.Copy(sourceFile, destFile, overwrite);
                    break;
                }
                catch (IOException) when (i < NUM_RETRIES - 1) {
                    Thread.Sleep(1000);
                }
                catch (IOException ioe) {
                    Debug.Print("A problem occurred when trying to create '" + destFile +"' from '" + sourceFile + "'.");
                    Debug.Print(ioe.ToString());
                }
            }
        }

        /**
         * Extracts a zip file to a folder.
         * 
         * 'fileName' is a given nickname for the resulting zip file, and is only used iin the
         * error message if an issue occurs.
         */
        private void extractZip(String zipFilepath, String destinationFolder, String fileName) {
            try {
                ZipFile.ExtractToDirectory(zipFilepath, destinationFolder);
            }
            catch (IOException) {
                Debug.Print("An error occurred when trying to install '" + fileName +"' to '"
                            + destinationFolder + "'. Do you already have this installed?");
            }

        }

        /**
         * Installs the requested files to the appropriate locations. These operations are done on
         * their own threads to allow the UI to be responsive while the user waits for the
         * installation to complete.
         */
        private async Task installFiles() {
            // Main destination directories for our installs
            String cfgPath = Path.Combine(tfPath, @"tf\cfg\");
            String customPath = Path.Combine(tfPath, @"tf\custom");
            String windowsFontsPath = Path.Combine(Environment.GetEnvironmentVariable("windir"), "Fonts");
                       
            // Initialize the progress bar before starting the installation
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Step = 1;

            // Adjust number of components to be moved based on user choice. If they have opted out
            // of a component, that's one less component that will be installed.
            progressBar.Maximum = NUM_COMPONENTS;
            progressBar.Maximum -= (installHUD) ? 0 : 2;
            progressBar.Maximum -= (installHitsound) ? 0 : 1;
            
            progressBar.Visible = true;

            // Unzip HUD, unless user opted out
            if (installHUD) {
                promptInstall.Text = "Installing the Improved Default HUD...";
                await Task.Run(() => {
                    String HUD_Zip = Path.Combine(basePath, @"custom-files\idhud-master.zip");
                    extractZip(HUD_Zip, customPath, "Improved Default HUD");
                });
                progressBar.PerformStep();

                promptInstall.Text = "Installing required fonts for the Improved Default HUD...";
                await Task.Run(() => {
                    // In order for idhud to work properly, some fonts need to be installed. These are
                    // provided in idhud's zip file.
                    String fontsPath = Path.Combine(customPath, @"idhud-master\resource\fonts");

                    // Copy each file over to the windows fonts directory to install them.
                    foreach (string font in Directory.GetFiles(fontsPath)) {
                        String destFile = Path.Combine(windowsFontsPath, Path.GetFileName(font));
                        copyFile(font, destFile);
                    }
                });
                progressBar.PerformStep();
            }

            // Unzip hitsound, unless user opted out
            if (installHitsound) {
                promptInstall.Text = "Installing hitsound files...";
                await Task.Run(() => {
                    String hitsoundZip = Path.Combine(basePath, @"custom-files\neodeafults-hitsound.zip");
                    extractZip(hitsoundZip, customPath, "Custom Quake hitsound");
                });
                progressBar.PerformStep();
            }

            // Copy new autoexec file
            promptInstall.Text = "Installing config files...";
            await Task.Run(() => {
                String sourceAutoexec = Path.Combine(basePath, autoexecSourceName);
                String destAutoexec = Path.Combine(cfgPath, autoexecDestName);
                copyFile(sourceAutoexec, destAutoexec);
            });
            progressBar.PerformStep();

            Thread.Sleep(100);
            promptInstall.Text = "Installation complete.";
            // Progress complete. Allow user to continue to next page
            nextInstall.Enabled = true;
        }

        /**
         * When navigating to the path page, the program needs to load elements based on whether or
         * not a TF2 install has been found. This will only be executed once.
         */
        private bool first = true;
        private void preparePathPanel() {
            // If the path to the TF2 install was found during startup, disable the ability for
            // the user to reset it. This is done only once to prevent strange issues when switching
            // between panels.
            if (!first)
                return;

            if (tfPath != null) {
                // Once the TF2 installation has been found, disallow it from being reset to prevent
                // potential issues.
                buttonPath.Enabled = false;
                promptPath.Text = "Found the path to the default TF2 install file. Proceed to the" 
                                  + " next page.";
            }
            else {
                promptPath.Text = "Select the location where you installed your game, and"
                + " find the \"hl2.exe\" file. \n\r"
                + "This is usually in a location that looks like:\n\r"
                + DEFAULT_TF2_PATH + "\\hl2.exe";
            }

            first = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e) {

        }

        /** 
         * General method for navigating to the next page in the setup menu.
         */
        private void next_Click(object sender, EventArgs e) {
            nextPanel();
        }

        /**
         * General method for navigating to the previous page in the setup menu.
         */
        private void prev_Click(object sender, EventArgs e) {
            previousPanel();
        }


        /**
         * Navigates to the previous page in the setup menu.
         */
        private void previousPanel() {
            updateScreen(stack.Pop());
        }

        /**
         * Navigates to the next page in the setup menu.
         */
        private async void nextPanel() {
            // Save current panel in case the user later wishes to go back.
            stack.Push(currentPanel);

            Panel nextPanel;
            if (currentPanel == panel1) {
                // Prepare elements on the next page before displaying anything.
                preparePathPanel();
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
                Application.Exit();
                return;
            }
            else {
                // It should not be possible to get an unknown panel onto the stack.
                Debug.Print("SEVERE ERROR: The current panel is currently set to '" 
                            + currentPanel.Name + "', which is not defined. This is not expected"
                            + " behavior.");
                return;
            }
            updateScreen(nextPanel);
            
            // Certain panels need to execute tasks immediately upon switching to the next page.
            if (currentPanel == panel5) {
                await installFiles();
            }
        }

        /**
         * Moves the provided panel to be displayed in the UI.
         */
        private void updateScreen(Panel newPanel) {
            // The current panel being viewed.
            Panel oldPanel = currentPanel;

            // Swap the two panels' locations. Since the main window is the size of one panel, this
            // will effectively make it appear that the new panel is just the next page.
            oldPanel.Location = newPanel.Location;
            newPanel.Location = new Point(0, 0);

            currentPanel = newPanel;
        }

        private void Main_Load(object sender, EventArgs e) {

        }

        private void label4_Click(object sender, EventArgs e) {

        }

        private void menu1Hitsound_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHitsound = true;
            }
        }

        private void menu1HUD_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHUD = true;
            }
        }

        private void progressBar_Click(object sender, EventArgs e) {

        }

        private void menu2Hitsound_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHitsound = false;
            }
        }

        private void menu2HUD_CheckedChanged(object sender, EventArgs e) {
            RadioButton element = (RadioButton) sender;
            if (element.Checked) {
                installHUD = false;
            }
        }
    }
}
