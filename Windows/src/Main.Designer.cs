using System;

namespace NeoDefaults_Installer{
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
            this.components = new System.ComponentModel.Container();
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
            this.backPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CfgBox = new System.Windows.Forms.CheckBox();
            this.HUDBox = new System.Windows.Forms.CheckBox();
            this.HitsoundBox = new System.Windows.Forms.CheckBox();
            this.NextOpt = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LabelOpt = new System.Windows.Forms.Label();
            this.BackOpt = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.progressBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.nextInstall = new System.Windows.Forms.Button();
            this.promptInstall = new System.Windows.Forms.Label();
            this.promptLast = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.imageLast = new System.Windows.Forms.PictureBox();
            this.exitLast = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageLast)).BeginInit();
            this.SuspendLayout();
            // 
            // menu1Home
            // 
            this.menu1Home.AccessibleDescription = "";
            this.menu1Home.AccessibleName = "";
            this.menu1Home.AutoSize = true;
            this.menu1Home.Checked = true;
            this.menu1Home.Location = new System.Drawing.Point(38, 166);
            this.menu1Home.Name = "menu1Home";
            this.menu1Home.Size = new System.Drawing.Size(239, 17);
            this.menu1Home.TabIndex = 0;
            this.menu1Home.TabStop = true;
            this.menu1Home.Text = "Basic install— Install all recommended options.";
            this.menu1Home.UseVisualStyleBackColor = true;
            this.menu1Home.CheckedChanged += new System.EventHandler(this.basic_install_CheckedChanged);
            // 
            // menu2Home
            // 
            this.menu2Home.AutoSize = true;
            this.menu2Home.Location = new System.Drawing.Point(38, 187);
            this.menu2Home.Name = "menu2Home";
            this.menu2Home.Size = new System.Drawing.Size(296, 17);
            this.menu2Home.TabIndex = 0;
            this.menu2Home.Text = "Advanced install— Customize which components to install.";
            this.menu2Home.UseVisualStyleBackColor = true;
            this.menu2Home.CheckedChanged += new System.EventHandler(this.advanced_install_CheckedChanged);
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(41, 152);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(100, 22);
            this.buttonPath.TabIndex = 1;
            this.buttonPath.Text = "Select Folder...";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.ButtonPath_Click);
            // 
            // promptPath
            // 
            this.promptPath.AutoSize = true;
            this.promptPath.Location = new System.Drawing.Point(39, 87);
            this.promptPath.MaximumSize = new System.Drawing.Size(540, 0);
            this.promptPath.Name = "promptPath";
            this.promptPath.Size = new System.Drawing.Size(235, 13);
            this.promptPath.TabIndex = 2;
            this.promptPath.Text = "Select the path to your \"Team Fortress 2\" folder:";
            // 
            // promptHome
            // 
            this.promptHome.AutoSize = true;
            this.promptHome.Location = new System.Drawing.Point(25, 142);
            this.promptHome.Name = "promptHome";
            this.promptHome.Size = new System.Drawing.Size(133, 13);
            this.promptHome.TabIndex = 2;
            this.promptHome.Text = "Select the installation type.";
            // 
            // nextHome
            // 
            this.nextHome.Location = new System.Drawing.Point(515, 385);
            this.nextHome.Name = "nextHome";
            this.nextHome.Size = new System.Drawing.Size(75, 22);
            this.nextHome.TabIndex = 1;
            this.nextHome.Text = "Next";
            this.nextHome.UseVisualStyleBackColor = true;
            this.nextHome.Click += new System.EventHandler(this.Next_Click);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Courier New", 27.75F);
            this.title.Location = new System.Drawing.Point(25, 58);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(567, 39);
            this.title.TabIndex = 2;
            this.title.Text = "TF2 NeoDefaults Installer";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonPathMessage
            // 
            this.buttonPathMessage.AutoSize = true;
            this.buttonPathMessage.ForeColor = System.Drawing.Color.DimGray;
            this.buttonPathMessage.Location = new System.Drawing.Point(148, 156);
            this.buttonPathMessage.Name = "buttonPathMessage";
            this.buttonPathMessage.Size = new System.Drawing.Size(75, 13);
            this.buttonPathMessage.TabIndex = 2;
            this.buttonPathMessage.Text = "%MESSAGE%";
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
            this.nextPath.Enabled = false;
            this.nextPath.Location = new System.Drawing.Point(515, 385);
            this.nextPath.Name = "nextPath";
            this.nextPath.Size = new System.Drawing.Size(75, 22);
            this.nextPath.TabIndex = 1;
            this.nextPath.Text = "Next";
            this.nextPath.UseVisualStyleBackColor = true;
            this.nextPath.Click += new System.EventHandler(this.Next_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.backPath);
            this.panel2.Controls.Add(this.nextPath);
            this.panel2.Controls.Add(this.promptPath);
            this.panel2.Controls.Add(this.buttonPathMessage);
            this.panel2.Controls.Add(this.buttonPath);
            this.panel2.Location = new System.Drawing.Point(645, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(640, 480);
            this.panel2.TabIndex = 3;
            // 
            // backPath
            // 
            this.backPath.Location = new System.Drawing.Point(434, 385);
            this.backPath.Name = "backPath";
            this.backPath.Size = new System.Drawing.Size(75, 22);
            this.backPath.TabIndex = 1;
            this.backPath.Text = "Back";
            this.backPath.UseVisualStyleBackColor = true;
            this.backPath.Click += new System.EventHandler(this.Prev_Click);
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
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.CfgBox);
            this.panel3.Controls.Add(this.HUDBox);
            this.panel3.Controls.Add(this.HitsoundBox);
            this.panel3.Controls.Add(this.NextOpt);
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.LabelOpt);
            this.panel3.Controls.Add(this.BackOpt);
            this.panel3.Location = new System.Drawing.Point(1290, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(640, 480);
            this.panel3.TabIndex = 3;
            // 
            // CfgBox
            // 
            this.CfgBox.AutoSize = true;
            this.CfgBox.Checked = true;
            this.CfgBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CfgBox.Location = new System.Drawing.Point(39, 152);
            this.CfgBox.Name = "CfgBox";
            this.CfgBox.Size = new System.Drawing.Size(117, 17);
            this.CfgBox.TabIndex = 6;
            this.CfgBox.Text = "NeoDefaults config";
            this.CfgBox.UseVisualStyleBackColor = true;
            this.CfgBox.CheckedChanged += new System.EventHandler(this.CfgBox_CheckedChanged);
            // 
            // HUDBox
            // 
            this.HUDBox.AutoSize = true;
            this.HUDBox.Checked = true;
            this.HUDBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HUDBox.Location = new System.Drawing.Point(39, 178);
            this.HUDBox.Name = "HUDBox";
            this.HUDBox.Size = new System.Drawing.Size(145, 17);
            this.HUDBox.TabIndex = 6;
            this.HUDBox.Text = "Custom damage numbers";
            this.HUDBox.UseVisualStyleBackColor = true;
            this.HUDBox.CheckedChanged += new System.EventHandler(this.HUDBox_CheckedChanged);
            // 
            // HitsoundBox
            // 
            this.HitsoundBox.AutoSize = true;
            this.HitsoundBox.Checked = true;
            this.HitsoundBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HitsoundBox.Location = new System.Drawing.Point(39, 204);
            this.HitsoundBox.Name = "HitsoundBox";
            this.HitsoundBox.Size = new System.Drawing.Size(120, 17);
            this.HitsoundBox.TabIndex = 6;
            this.HitsoundBox.Text = "Custom hitsound file";
            this.HitsoundBox.UseVisualStyleBackColor = true;
            this.HitsoundBox.CheckedChanged += new System.EventHandler(this.HitsoundBox_CheckedChanged);
            // 
            // NextOpt
            // 
            this.NextOpt.Location = new System.Drawing.Point(515, 385);
            this.NextOpt.Name = "NextOpt";
            this.NextOpt.Size = new System.Drawing.Size(75, 22);
            this.NextOpt.TabIndex = 1;
            this.NextOpt.Text = "Install";
            this.NextOpt.UseVisualStyleBackColor = true;
            this.NextOpt.Click += new System.EventHandler(this.Next_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(156, 149);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox3, "These config files are responsible for changing in-game settings like FOV.");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(184, 175);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox2, "Increases the size of damage numbers to make them much easier to read.  This plug" +
        "in may not work if you have a custom HUD installed.");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(159, 201);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox1, "The hitsound settings in the NeoDefaults config have been specifically tuned for " +
        "this sound file. \r\nIf the config is not installed, you will need to enable hitso" +
        "unds manually.");
            // 
            // LabelOpt
            // 
            this.LabelOpt.AutoSize = true;
            this.LabelOpt.Location = new System.Drawing.Point(39, 84);
            this.LabelOpt.MaximumSize = new System.Drawing.Size(550, 0);
            this.LabelOpt.Name = "LabelOpt";
            this.LabelOpt.Size = new System.Drawing.Size(539, 26);
            this.LabelOpt.TabIndex = 2;
            this.LabelOpt.Text = resources.GetString("LabelOpt.Text");
            // 
            // BackOpt
            // 
            this.BackOpt.Location = new System.Drawing.Point(434, 385);
            this.BackOpt.Name = "BackOpt";
            this.BackOpt.Size = new System.Drawing.Size(75, 22);
            this.BackOpt.TabIndex = 1;
            this.BackOpt.Text = "Back";
            this.BackOpt.UseVisualStyleBackColor = true;
            this.BackOpt.Click += new System.EventHandler(this.Prev_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.progressBox);
            this.panel5.Controls.Add(this.progressBar);
            this.panel5.Controls.Add(this.nextInstall);
            this.panel5.Controls.Add(this.promptInstall);
            this.panel5.Location = new System.Drawing.Point(645, 485);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(640, 480);
            this.panel5.TabIndex = 3;
            // 
            // progressBox
            // 
            this.progressBox.Location = new System.Drawing.Point(38, 135);
            this.progressBox.Multiline = true;
            this.progressBox.Name = "progressBox";
            this.progressBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.progressBox.Size = new System.Drawing.Size(550, 231);
            this.progressBox.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(38, 109);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(550, 20);
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            // 
            // nextInstall
            // 
            this.nextInstall.Enabled = false;
            this.nextInstall.Location = new System.Drawing.Point(515, 385);
            this.nextInstall.Name = "nextInstall";
            this.nextInstall.Size = new System.Drawing.Size(75, 22);
            this.nextInstall.TabIndex = 1;
            this.nextInstall.Text = "Finish";
            this.nextInstall.UseVisualStyleBackColor = true;
            this.nextInstall.Click += new System.EventHandler(this.Next_Click);
            // 
            // promptInstall
            // 
            this.promptInstall.AutoSize = true;
            this.promptInstall.Location = new System.Drawing.Point(39, 87);
            this.promptInstall.Name = "promptInstall";
            this.promptInstall.Size = new System.Drawing.Size(115, 13);
            this.promptInstall.TabIndex = 2;
            this.promptInstall.Text = "Beginning installation...";
            // 
            // promptLast
            // 
            this.promptLast.AutoSize = true;
            this.promptLast.Location = new System.Drawing.Point(89, 87);
            this.promptLast.MaximumSize = new System.Drawing.Size(450, 0);
            this.promptLast.Name = "promptLast";
            this.promptLast.Size = new System.Drawing.Size(105, 13);
            this.promptLast.TabIndex = 2;
            this.promptLast.Text = "%EXIT_MESSAGE%";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.imageLast);
            this.panel6.Controls.Add(this.exitLast);
            this.panel6.Controls.Add(this.promptLast);
            this.panel6.Location = new System.Drawing.Point(1290, 485);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(640, 480);
            this.panel6.TabIndex = 3;
            // 
            // imageLast
            // 
            this.imageLast.Location = new System.Drawing.Point(42, 87);
            this.imageLast.Name = "imageLast";
            this.imageLast.Size = new System.Drawing.Size(50, 50);
            this.imageLast.TabIndex = 10;
            this.imageLast.TabStop = false;
            // 
            // exitLast
            // 
            this.exitLast.Location = new System.Drawing.Point(515, 385);
            this.exitLast.Name = "exitLast";
            this.exitLast.Size = new System.Drawing.Size(75, 22);
            this.exitLast.TabIndex = 1;
            this.exitLast.Text = "Exit";
            this.exitLast.UseVisualStyleBackColor = true;
            this.exitLast.Click += new System.EventHandler(this.Next_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 15000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(2048, 975);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "NeoDefaults Installer v";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageLast)).EndInit();
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
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button nextInstall;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button exitLast;
        private System.Windows.Forms.Label LabelOpt;
        private System.Windows.Forms.Label promptInstall;
        private System.Windows.Forms.Label promptLast;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button NextOpt;
        private System.Windows.Forms.Button backPath;
        private System.Windows.Forms.Button BackOpt;
        private System.Windows.Forms.TextBox progressBox;
        private System.Windows.Forms.PictureBox imageLast;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox HUDBox;
        private System.Windows.Forms.CheckBox HitsoundBox;
        private System.Windows.Forms.CheckBox CfgBox;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

