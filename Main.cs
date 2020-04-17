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

namespace CfgInstallerPrototype {
    public partial class Main : Form {
        private readonly bool DEVELOP_MODE = true;

        // Defines the default size of the main screen.
        private readonly Size DEFAULT_WINDOW_SIZE = new Size(640, 480);

        // The autoexec file to copy 
        private readonly String autoexecSourceName = "autoexec-alpha.cfg";
        private readonly String autoexecDestName = "autoexec-TEST.cfg";

        // TF-path related parameters
        private readonly String DEFAULT_TF2_PATH = @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2";
        private readonly String myPath = @"E:\Steam\SteamApps\common\Team Fortress 2";
        private readonly String basePath;
        private String tfPath;

        // A reference to the panel object that is currently being displayed on the main screen.
        private Panel currentPanel;

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
            String parentPath = (DEVELOP_MODE) ? @"\..\..\..\.." : @"\..";
            
            String relativeBasePath = AppDomain.CurrentDomain.BaseDirectory;
            relativeBasePath = Path.Combine(relativeBasePath, parentPath);
            basePath = new FileInfo(relativeBasePath).FullName;
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
                }
            }


            setTFPath(filePath);
            tfPath = filePath;

            

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {

        }

        private void copyAutoexec(AsyncVoidMethodBuilder a) {
            String cfgPath = Path.Combine(tfPath, @"tf\cfg\");
            String sourceFile = Path.Combine(basePath, autoexecSourceName);
            String destFile = Path.Combine(cfgPath, autoexecDestName);

            // If an autoexec already exists, issue an alert.
            bool continueAnyway = false;
            if (File.Exists(destFile)) {
                Form dlg1 = new Form();
                dlg1.ShowDialog();
                
                // if they select "continue anyway", set the variable
                // continueAnyway = true;
            }
            File.Copy(sourceFile, destFile);

            return;
        }

        // Validates and stores the path to the "Team Fortress" folder. Returns true if the operation was successful.
        private bool setTFPath(String path) {

            return true;
        }

        private void nextButton_Click(object sender, EventArgs e) {
            updateScreen(panel2);
            
            
            
            
            // First check that we haven't stored the filepath on a previous run
            // if (File.Exists("myfilename")) {
            //     also double-check that TF2 is still installed there
            //     setTFPath("the path from the file");
            // }

        }

        private void panel2_Paint(object sender, PaintEventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            updateScreen(panel3);
        }

        private void button3_Click(object sender, EventArgs e) {
            // For now, go back to home
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
    }
}
