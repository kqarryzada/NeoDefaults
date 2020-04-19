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
        //private readonly String DEFAULT_TF2_PATH = @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2";
        private readonly String DEFAULT_TF2_PATH = @"E:\Steam\SteamApps\common\Team Fortress 2";
        private readonly String basePath;
        private String tfPath;

        // The approximate number of components expected to be installed. This includes elements
        // like the autoexec file, the HUD, etc.
        private readonly int NUM_COMPONENTS = 6;

        // A reference to the panel object that is currently being displayed on the main screen.
        private Panel currentPanel;

        // Specifies whether user requested additional customizations.
        private bool installHUD = true;
        private bool installHitsound = true;

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

            // On startup, check for a TF2 installation at 'DEFAULT_TF2_PATH'.
            checkDefaultTF2Install();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {

        }

        private void basic_install_CheckedChanged(object sender, EventArgs e) {

        }

        private void tf2_path_button_description_Click(object sender, EventArgs e) {

        }

        private bool checkDefaultTF2Install() {
            // Check to see if the "hl2.exe" binary is in the default location.
            if (File.Exists(Path.Combine(DEFAULT_TF2_PATH, "hl2.exe"))) {
                tfPath = DEFAULT_TF2_PATH;
                return true;
            }

            return false;
        }

        private void pathButton_Click(object sender, EventArgs e) {
            String filePath = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Program files (*.exe)|*.exe|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                // openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    // Get the path of specified file
                    filePath = openFileDialog.FileName;

                    // Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    // using (StreamReader reader = new StreamReader(fileStream)) {
                    //     // fileContent = reader.ReadToEnd();
                    // }

                    filePath = openFileDialog.FileName;

                    // Since hl2.exe is selected, point 'filePath' at the parent directory
                    filePath = Path.Combine(filePath, "..");
                }
            }
            folderPrompt.Text = basePath;
            folderPrompt.Visible = true;
            tfPath = filePath;

            

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {

        }


        private void copyFile(String sourceFile, String destFile) {
            copyFile(sourceFile, destFile, false);
        }

        private readonly String backupFolder = @"";
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
                    //Debug.Print(ioe.ToString());
                }
            }
        }

        private void extractZip(String zipFilepath, String destinationFolder, String fileName) {
            try {
                ZipFile.ExtractToDirectory(zipFilepath, destinationFolder);
            }
            catch (IOException) {
                Debug.Print("An error occurred when trying to install '" + fileName +"' to '"
                            + destinationFolder + "'. Do you already have this installed?");
            }

        }
        private void installFiles() {
            // Main destination directories for our installs
            String cfgPath = Path.Combine(tfPath, @"tf\cfg\");
            String customPath = Path.Combine(tfPath, @"tf\custom");
            String windowsFontsPath = Path.Combine(Environment.GetEnvironmentVariable("windir"), "Fonts");
                       
            // Initialize progress bar before beginning install
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Step = 1;

            // Adjust number of components to be moved based on user choice. If they have opted out
            // of a component, that's one less component that will be installed.
            progressBar.Maximum = NUM_COMPONENTS;
            progressBar.Maximum -= (installHUD) ? 0 : 1;
            progressBar.Maximum -= (installHitsound) ? 0 : 1;
            
            progressBar.Visible = true;

            // Copy new autoexec file
            String sourceAutoexec = Path.Combine(basePath, autoexecSourceName);
            String destAutoexec = Path.Combine(cfgPath, autoexecDestName);
            copyFile(sourceAutoexec, destAutoexec);
            progressBar.PerformStep();

            // Unzip HUD and hitsound, unless user opted out
            String HUD_Zip = Path.Combine(basePath, @"custom-files\idhud-master.zip");
            String hitsoundZip = Path.Combine(basePath, @"custom-files\neodeafults-hitsound.zip");
            if (installHUD) {
                extractZip(HUD_Zip, customPath, "Improved Default HUD");
                progressBar.PerformStep();
            }
            if (installHitsound) {
                extractZip(hitsoundZip, customPath, "Custom Quake hitsound");
                progressBar.PerformStep();
            }

            // In order for idhud to work properly, some fonts need to be installed. These are
            // provided in idhud's zip file.
            String fontsPath = Path.Combine(customPath, @"idhud-master\resource\fonts");

            // Copy each file over to the windows fonts directory to install them.
            foreach (string font in Directory.GetFiles(fontsPath)) {
                String destFile = Path.Combine(windowsFontsPath, Path.GetFileName(font));
                copyFile(font, destFile);
                progressBar.PerformStep();
            }

            label9.Visible = true;
            button4.Enabled = true;

        }

        private void nextButton_Click(object sender, EventArgs e) {
            // If the path to the TF2 install was found during startup, disable the ability for
            // the user to reset it.
            if (tfPath != null) {
                panel2Description.Text = "Found the path to the default TF2 install file.";

                pathButton.Enabled = false;
            }
            else {
                panel2Description.Text = "Select the location where you installed your game, and"
                + " find the \"hl2.exe\" file. \n\r"
                + "This is usually in a location that looks like:\n\r"
                + DEFAULT_TF2_PATH;
            }

            updateScreen(panel2);
        }

        private void panel2_Paint(object sender, PaintEventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            updateScreen(panel3);
        }

        private void button3_Click(object sender, EventArgs e) {
            updateScreen(panel4);
            installFiles();
        }


        private void button4_Click(object sender, EventArgs e) {
            updateScreen(panel5);
        }


        private void button5_Click(object sender, EventArgs e) {
            // For now, go to home screen
            updateScreen(panel1);
        }



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

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {

        }

        private void progressBar1_Click(object sender, EventArgs e) {

        }
    }
}
