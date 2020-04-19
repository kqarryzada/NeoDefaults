namespace CfgInstallerPrototype {
    partial class Main {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menu1Home = new System.Windows.Forms.RadioButton();
            this.menu2Home = new System.Windows.Forms.RadioButton();
            this.buttonPath = new System.Windows.Forms.Button();
            this.promptPath = new System.Windows.Forms.Label();
            this.promptHome = new System.Windows.Forms.Label();
            this.nextHome = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonPathMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nextPath = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.menu2Hitsound = new System.Windows.Forms.RadioButton();
            this.menu1Hitsound = new System.Windows.Forms.RadioButton();
            this.promptHitsound = new System.Windows.Forms.Label();
            this.nextHitsound = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.nextInstall = new System.Windows.Forms.Button();
            this.promptInstall = new System.Windows.Forms.Label();
            this.promptLast = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.exitLast = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.menu2HUD = new System.Windows.Forms.RadioButton();
            this.menu1HUD = new System.Windows.Forms.RadioButton();
            this.promptHUD = new System.Windows.Forms.Label();
            this.nextHUD = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu1Home
            // 
            this.menu1Home.AccessibleDescription = "";
            this.menu1Home.AccessibleName = "";
            this.menu1Home.AutoSize = true;
            this.menu1Home.Checked = true;
            this.menu1Home.Location = new System.Drawing.Point(44, 191);
            this.menu1Home.Name = "menu1Home";
            this.menu1Home.Size = new System.Drawing.Size(274, 19);
            this.menu1Home.TabIndex = 0;
            this.menu1Home.TabStop = true;
            this.menu1Home.Text = "Basic install— Install all recommended options.";
            this.menu1Home.UseVisualStyleBackColor = true;
            this.menu1Home.CheckedChanged += new System.EventHandler(this.basic_install_CheckedChanged);
            // 
            // menu2Home
            // 
            this.menu2Home.AutoSize = true;
            this.menu2Home.Location = new System.Drawing.Point(44, 216);
            this.menu2Home.Name = "menu2Home";
            this.menu2Home.Size = new System.Drawing.Size(339, 19);
            this.menu2Home.TabIndex = 0;
            this.menu2Home.Text = "Advanced install— Customize which components to install.";
            this.menu2Home.UseVisualStyleBackColor = true;
            this.menu2Home.CheckedChanged += new System.EventHandler(this.advanced_install_CheckedChanged);
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(48, 175);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(103, 23);
            this.buttonPath.TabIndex = 1;
            this.buttonPath.Text = "Select Folder...";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // promptPath
            // 
            this.promptPath.AutoSize = true;
            this.promptPath.Location = new System.Drawing.Point(48, 123);
            this.promptPath.Name = "promptPath";
            this.promptPath.Size = new System.Drawing.Size(257, 15);
            this.promptPath.TabIndex = 2;
            this.promptPath.Text = "Select the path to your \"Team Fortress 2\" folder:";
            this.promptPath.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // promptHome
            // 
            this.promptHome.AutoSize = true;
            this.promptHome.Location = new System.Drawing.Point(29, 164);
            this.promptHome.Name = "promptHome";
            this.promptHome.Size = new System.Drawing.Size(148, 15);
            this.promptHome.TabIndex = 2;
            this.promptHome.Text = "Select the installation type.";
            this.promptHome.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // nextHome
            // 
            this.nextHome.Location = new System.Drawing.Point(495, 390);
            this.nextHome.Name = "nextHome";
            this.nextHome.Size = new System.Drawing.Size(87, 23);
            this.nextHome.TabIndex = 1;
            this.nextHome.Text = "Next";
            this.nextHome.UseVisualStyleBackColor = true;
            this.nextHome.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.title.Location = new System.Drawing.Point(29, 67);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(567, 39);
            this.title.TabIndex = 2;
            this.title.Text = "TF2 NeoDefaults Installer";
            this.title.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // buttonPathMessage
            // 
            this.buttonPathMessage.AutoSize = true;
            this.buttonPathMessage.ForeColor = System.Drawing.Color.DimGray;
            this.buttonPathMessage.Location = new System.Drawing.Point(157, 179);
            this.buttonPathMessage.Name = "buttonPathMessage";
            this.buttonPathMessage.Size = new System.Drawing.Size(78, 15);
            this.buttonPathMessage.TabIndex = 2;
            this.buttonPathMessage.Text = "%MESSAGE%";
            this.buttonPathMessage.Visible = false;
            this.buttonPathMessage.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.promptHome);
            this.panel1.Controls.Add(this.title);
            this.panel1.Controls.Add(this.menu1Home);
            this.panel1.Controls.Add(this.menu2Home);
            this.panel1.Controls.Add(this.nextHome);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 480);
            this.panel1.TabIndex = 3;
            // 
            // nextPath
            // 
            this.nextPath.Location = new System.Drawing.Point(495, 390);
            this.nextPath.Name = "nextPath";
            this.nextPath.Size = new System.Drawing.Size(87, 23);
            this.nextPath.TabIndex = 1;
            this.nextPath.Text = "Next";
            this.nextPath.UseVisualStyleBackColor = true;
            this.nextPath.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nextPath);
            this.panel2.Controls.Add(this.promptPath);
            this.panel2.Controls.Add(this.buttonPathMessage);
            this.panel2.Controls.Add(this.buttonPath);
            this.panel2.Location = new System.Drawing.Point(645, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(640, 480);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            // 
            // radioButton1
            // 
            this.radioButton1.Location = new System.Drawing.Point(0, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(104, 24);
            this.radioButton1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(0, 0);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(104, 24);
            this.radioButton2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(509, 384);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.menu2Hitsound);
            this.panel3.Controls.Add(this.menu1Hitsound);
            this.panel3.Controls.Add(this.promptHitsound);
            this.panel3.Controls.Add(this.nextHitsound);
            this.panel3.Location = new System.Drawing.Point(1290, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(640, 480);
            this.panel3.TabIndex = 3;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // menu2Hitsound
            // 
            this.menu2Hitsound.AutoSize = true;
            this.menu2Hitsound.Location = new System.Drawing.Point(49, 175);
            this.menu2Hitsound.Name = "menu2Hitsound";
            this.menu2Hitsound.Size = new System.Drawing.Size(41, 19);
            this.menu2Hitsound.TabIndex = 3;
            this.menu2Hitsound.Text = "No";
            this.menu2Hitsound.UseVisualStyleBackColor = true;
            this.menu2Hitsound.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // menu1Hitsound
            // 
            this.menu1Hitsound.AutoSize = true;
            this.menu1Hitsound.Checked = true;
            this.menu1Hitsound.Location = new System.Drawing.Point(49, 150);
            this.menu1Hitsound.Name = "menu1Hitsound";
            this.menu1Hitsound.Size = new System.Drawing.Size(134, 19);
            this.menu1Hitsound.TabIndex = 3;
            this.menu1Hitsound.TabStop = true;
            this.menu1Hitsound.Text = "Yes (Recommended)";
            this.menu1Hitsound.UseVisualStyleBackColor = true;
            this.menu1Hitsound.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // promptHitsound
            // 
            this.promptHitsound.AutoSize = true;
            this.promptHitsound.Location = new System.Drawing.Point(49, 123);
            this.promptHitsound.Name = "promptHitsound";
            this.promptHitsound.Size = new System.Drawing.Size(130, 15);
            this.promptHitsound.TabIndex = 2;
            this.promptHitsound.Text = "Install Quake hitsound?";
            this.promptHitsound.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // nextHitsound
            // 
            this.nextHitsound.Location = new System.Drawing.Point(495, 390);
            this.nextHitsound.Name = "nextHitsound";
            this.nextHitsound.Size = new System.Drawing.Size(87, 23);
            this.nextHitsound.TabIndex = 1;
            this.nextHitsound.Text = "Next";
            this.nextHitsound.UseVisualStyleBackColor = true;
            this.nextHitsound.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.progressBar);
            this.panel5.Controls.Add(this.nextInstall);
            this.panel5.Controls.Add(this.promptInstall);
            this.panel5.Location = new System.Drawing.Point(645, 485);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(640, 480);
            this.panel5.TabIndex = 3;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(44, 126);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(538, 23);
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            this.progressBar.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // nextInstall
            // 
            this.nextInstall.Enabled = false;
            this.nextInstall.Location = new System.Drawing.Point(495, 390);
            this.nextInstall.Name = "nextInstall";
            this.nextInstall.Size = new System.Drawing.Size(87, 23);
            this.nextInstall.TabIndex = 1;
            this.nextInstall.Text = "Next";
            this.nextInstall.UseVisualStyleBackColor = true;
            this.nextInstall.Click += new System.EventHandler(this.button5_Click);
            // 
            // promptInstall
            // 
            this.promptInstall.AutoSize = true;
            this.promptInstall.Location = new System.Drawing.Point(44, 98);
            this.promptInstall.Name = "promptInstall";
            this.promptInstall.Size = new System.Drawing.Size(131, 15);
            this.promptInstall.TabIndex = 2;
            this.promptInstall.Text = "Beginning installation...";
            this.promptInstall.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // promptLast
            // 
            this.promptLast.AutoSize = true;
            this.promptLast.Location = new System.Drawing.Point(49, 98);
            this.promptLast.Name = "promptLast";
            this.promptLast.Size = new System.Drawing.Size(298, 15);
            this.promptLast.TabIndex = 2;
            this.promptLast.Text = "Congrations! You done it. All files successfully installed.";
            this.promptLast.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.exitLast);
            this.panel6.Controls.Add(this.promptLast);
            this.panel6.Location = new System.Drawing.Point(1290, 485);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(640, 480);
            this.panel6.TabIndex = 3;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // exitLast
            // 
            this.exitLast.Location = new System.Drawing.Point(495, 390);
            this.exitLast.Name = "exitLast";
            this.exitLast.Size = new System.Drawing.Size(87, 23);
            this.exitLast.TabIndex = 1;
            this.exitLast.Text = "Exit";
            this.exitLast.UseVisualStyleBackColor = true;
            this.exitLast.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.menu2HUD);
            this.panel4.Controls.Add(this.menu1HUD);
            this.panel4.Controls.Add(this.promptHUD);
            this.panel4.Controls.Add(this.nextHUD);
            this.panel4.Location = new System.Drawing.Point(0, 485);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(640, 480);
            this.panel4.TabIndex = 3;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // menu2HUD
            // 
            this.menu2HUD.AutoSize = true;
            this.menu2HUD.Location = new System.Drawing.Point(49, 175);
            this.menu2HUD.Name = "menu2HUD";
            this.menu2HUD.Size = new System.Drawing.Size(41, 19);
            this.menu2HUD.TabIndex = 3;
            this.menu2HUD.Text = "No";
            this.menu2HUD.UseVisualStyleBackColor = true;
            this.menu2HUD.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // menu1HUD
            // 
            this.menu1HUD.AutoSize = true;
            this.menu1HUD.Checked = true;
            this.menu1HUD.Location = new System.Drawing.Point(49, 150);
            this.menu1HUD.Name = "menu1HUD";
            this.menu1HUD.Size = new System.Drawing.Size(134, 19);
            this.menu1HUD.TabIndex = 3;
            this.menu1HUD.TabStop = true;
            this.menu1HUD.Text = "Yes (Recommended)";
            this.menu1HUD.UseVisualStyleBackColor = true;
            this.menu1HUD.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // promptHUD
            // 
            this.promptHUD.AutoSize = true;
            this.promptHUD.Location = new System.Drawing.Point(49, 123);
            this.promptHUD.Name = "promptHUD";
            this.promptHUD.Size = new System.Drawing.Size(81, 15);
            this.promptHUD.TabIndex = 2;
            this.promptHUD.Text = "Install idHUD?";
            this.promptHUD.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // nextHUD
            // 
            this.nextHUD.Location = new System.Drawing.Point(495, 390);
            this.nextHUD.Name = "nextHUD";
            this.nextHUD.Size = new System.Drawing.Size(87, 23);
            this.nextHUD.TabIndex = 1;
            this.nextHUD.Text = "Next";
            this.nextHUD.UseVisualStyleBackColor = true;
            this.nextHUD.Click += new System.EventHandler(this.button4_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1955, 1046);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Project-name";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton menu1Home;
        private System.Windows.Forms.RadioButton menu2Home;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Label promptPath;
        private System.Windows.Forms.Label promptHome;
        private System.Windows.Forms.Button nextHome;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label buttonPathMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button nextPath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button nextHitsound;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button nextInstall;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button exitLast;
        private System.Windows.Forms.Label promptHitsound;
        private System.Windows.Forms.RadioButton menu2Hitsound;
        private System.Windows.Forms.RadioButton menu1Hitsound;
        private System.Windows.Forms.Label promptInstall;
        private System.Windows.Forms.Label promptLast;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton menu2HUD;
        private System.Windows.Forms.RadioButton menu1HUD;
        private System.Windows.Forms.Label promptHUD;
        private System.Windows.Forms.Button nextHUD;
    }
}

