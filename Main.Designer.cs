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
            this.basicInstall = new System.Windows.Forms.RadioButton();
            this.advancedInstall = new System.Windows.Forms.RadioButton();
            this.pathButton = new System.Windows.Forms.Button();
            this.panel2Description = new System.Windows.Forms.Label();
            this.installationPrompt = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.message1 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderPrompt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // basicInstall
            // 
            this.basicInstall.AccessibleDescription = "";
            this.basicInstall.AccessibleName = "";
            this.basicInstall.AutoSize = true;
            this.basicInstall.Checked = true;
            this.basicInstall.Location = new System.Drawing.Point(44, 191);
            this.basicInstall.Name = "basicInstall";
            this.basicInstall.Size = new System.Drawing.Size(86, 19);
            this.basicInstall.TabIndex = 0;
            this.basicInstall.TabStop = true;
            this.basicInstall.Text = "Basic Install";
            this.basicInstall.UseVisualStyleBackColor = true;
            this.basicInstall.CheckedChanged += new System.EventHandler(this.basic_install_CheckedChanged);
            // 
            // advancedInstall
            // 
            this.advancedInstall.AutoSize = true;
            this.advancedInstall.Enabled = false;
            this.advancedInstall.Location = new System.Drawing.Point(44, 216);
            this.advancedInstall.Name = "advancedInstall";
            this.advancedInstall.Size = new System.Drawing.Size(112, 19);
            this.advancedInstall.TabIndex = 0;
            this.advancedInstall.Text = "Advanced Install";
            this.advancedInstall.UseVisualStyleBackColor = true;
            this.advancedInstall.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // pathButton
            // 
            this.pathButton.Location = new System.Drawing.Point(48, 175);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(103, 23);
            this.pathButton.TabIndex = 1;
            this.pathButton.Text = "Select Folder...";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // panel2Description
            // 
            this.panel2Description.AutoSize = true;
            this.panel2Description.Location = new System.Drawing.Point(48, 123);
            this.panel2Description.Name = "panel2Description";
            this.panel2Description.Size = new System.Drawing.Size(257, 15);
            this.panel2Description.TabIndex = 2;
            this.panel2Description.Text = "Select the path to your \"Team Fortress 2\" folder:";
            this.panel2Description.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // installationPrompt
            // 
            this.installationPrompt.AutoSize = true;
            this.installationPrompt.Location = new System.Drawing.Point(29, 164);
            this.installationPrompt.Name = "installationPrompt";
            this.installationPrompt.Size = new System.Drawing.Size(148, 15);
            this.installationPrompt.TabIndex = 2;
            this.installationPrompt.Text = "Select the installation type.";
            this.installationPrompt.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(495, 390);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(87, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // message1
            // 
            this.message1.AutoSize = true;
            this.message1.Location = new System.Drawing.Point(176, 218);
            this.message1.Name = "message1";
            this.message1.Size = new System.Drawing.Size(225, 15);
            this.message1.TabIndex = 2;
            this.message1.Text = "Will be made available in a future update.";
            this.message1.Click += new System.EventHandler(this.tf2_path_button_description_Click);
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
            // folderPrompt
            // 
            this.folderPrompt.AutoSize = true;
            this.folderPrompt.ForeColor = System.Drawing.Color.DimGray;
            this.folderPrompt.Location = new System.Drawing.Point(157, 179);
            this.folderPrompt.Name = "folderPrompt";
            this.folderPrompt.Size = new System.Drawing.Size(78, 15);
            this.folderPrompt.TabIndex = 2;
            this.folderPrompt.Text = "%MESSAGE%";
            this.folderPrompt.Visible = false;
            this.folderPrompt.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.installationPrompt);
            this.panel1.Controls.Add(this.title);
            this.panel1.Controls.Add(this.basicInstall);
            this.panel1.Controls.Add(this.message1);
            this.panel1.Controls.Add(this.advancedInstall);
            this.panel1.Controls.Add(this.nextButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 480);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(495, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.panel2Description);
            this.panel2.Controls.Add(this.folderPrompt);
            this.panel2.Controls.Add(this.pathButton);
            this.panel2.Location = new System.Drawing.Point(645, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(640, 480);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(157, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(361, 25);
            this.label6.TabIndex = 2;
            this.label6.Text = "Additional recommended customization";
            this.label6.Click += new System.EventHandler(this.label4_Click);
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
            this.panel3.Controls.Add(this.radioButton4);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.radioButton5);
            this.panel3.Controls.Add(this.radioButton3);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Location = new System.Drawing.Point(1290, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(640, 480);
            this.panel3.TabIndex = 3;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(49, 175);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(41, 19);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "No";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(213, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Additional customizations";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(49, 200);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(201, 19);
            this.radioButton5.TabIndex = 3;
            this.radioButton5.Text = "I don\'t really know/care, just do it";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(49, 150);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(134, 19);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Yes (Recommended)";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Install Quake hitsound";
            this.label7.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(495, 390);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Next";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.progressBar);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(0, 486);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(640, 480);
            this.panel4.TabIndex = 3;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
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
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(495, 390);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Next";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Beginning installation...";
            this.label8.Visible = false;
            this.label8.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(48, 157);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "Installation complete!";
            this.label9.Visible = false;
            this.label9.Click += new System.EventHandler(this.tf2_path_button_description_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.button5);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Location = new System.Drawing.Point(645, 486);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(640, 480);
            this.panel5.TabIndex = 3;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(273, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "Page 5";
            this.label5.Click += new System.EventHandler(this.label4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(495, 390);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "Next";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1955, 1046);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
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
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton basicInstall;
        private System.Windows.Forms.RadioButton advancedInstall;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.Label panel2Description;
        private System.Windows.Forms.Label installationPrompt;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label message1;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label folderPrompt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar;

    }
}

