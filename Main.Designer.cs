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
            this.tf2PathButtonDescription = new System.Windows.Forms.Label();
            this.installationPrompt = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.message1 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderPrompt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.pathButton.Location = new System.Drawing.Point(48, 151);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(103, 23);
            this.pathButton.TabIndex = 1;
            this.pathButton.Text = "Select Folder...";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // tf2PathButtonDescription
            // 
            this.tf2PathButtonDescription.AutoSize = true;
            this.tf2PathButtonDescription.Location = new System.Drawing.Point(48, 123);
            this.tf2PathButtonDescription.Name = "tf2PathButtonDescription";
            this.tf2PathButtonDescription.Size = new System.Drawing.Size(257, 15);
            this.tf2PathButtonDescription.TabIndex = 2;
            this.tf2PathButtonDescription.Text = "Select the path to your \"Team Fortress 2\" folder:";
            this.tf2PathButtonDescription.Click += new System.EventHandler(this.tf2_path_button_description_Click);
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
            this.nextButton.Location = new System.Drawing.Point(509, 384);
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
            this.folderPrompt.Location = new System.Drawing.Point(157, 155);
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
            this.panel1.Location = new System.Drawing.Point(1165, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 480);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 384);
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
            this.panel2.Controls.Add(this.tf2PathButtonDescription);
            this.panel2.Controls.Add(this.folderPrompt);
            this.panel2.Controls.Add(this.pathButton);
            this.panel2.Location = new System.Drawing.Point(1165, 513);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1955, 1046);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton basicInstall;
        private System.Windows.Forms.RadioButton advancedInstall;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.Label tf2PathButtonDescription;
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
    }
}

