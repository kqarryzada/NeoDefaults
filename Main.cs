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
    public enum panelNumber {
        HOME_PAGE,
        PATH_PAGE,
    }

    public partial class Main : Form {
        private readonly bool DEVELOP_MODE = true;
        private readonly String DEFAULT_TF2_PATH = @"C:\Program Files (x86)\Steam\SteamApps\common\Team Fortress 2";
        private readonly String autoexecSourceName = "autoexec-alpha.cfg";
        private readonly String autoexecDestName = "autoexec-TEST.cfg";
        private readonly String myPath = @"E:\Steam\SteamApps\common\Team Fortress 2";
        private String tfPath;
        private String basePath;

        private Panel currentPanel;

        public Main() {
            InitializeComponent();
            currentPanel = panel1;

            // Hide other panels
            this.Size = new System.Drawing.Size(640, 480);

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

        private void pathButton_Click(object sender, EventArgs e) {
            /*
            Form dlg1 = new Form();
            dlg1.ShowDialog();
            */
            String filePath = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    /*
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream)) {
                        fileContent = reader.ReadToEnd();
                    }
                    */

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


            // store the provided path in a hidden file, maybe under %local%/neodefaults or something?
            
            folderPrompt.Text = "Team Fortress 2 install files found.";
            folderPrompt.Visible = true;
            pathButton.Enabled = false;

            return true;
        }

        private void nextButton_Click(object sender, EventArgs e) {
            updateScreen(panel2);
            
            
            
            
            // First check that we haven't stored the filepath on a previous run
            // if (File.Exists("myfilename")) {
            //     also double-check that TF2 is still installed there
            //     setTFPath("the path from the file");
            // }

            // Check for TF2 install in the most commonly installed place. If it exists in the 
            // default path, there's no need to query the user.
            String defaultTF2Install = Path.Combine(myPath, "hl2.exe");
            if (File.Exists(defaultTF2Install)) {
                setTFPath(DEFAULT_TF2_PATH);
            }
            else {

            }
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
