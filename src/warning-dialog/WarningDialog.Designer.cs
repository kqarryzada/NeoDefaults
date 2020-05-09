namespace NeoDefaults_Installer.warning_dialog {
    partial class WarningDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningDialog));
            this.DisplayImage = new System.Windows.Forms.PictureBox();
            this.WarningMessage = new System.Windows.Forms.Label();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.SkipButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayImage)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayImage
            // 
            this.DisplayImage.Location = new System.Drawing.Point(21, 27);
            this.DisplayImage.Name = "DisplayImage";
            this.DisplayImage.Size = new System.Drawing.Size(50, 50);
            this.DisplayImage.TabIndex = 9;
            this.DisplayImage.TabStop = false;
            // 
            // WarningMessage
            // 
            this.WarningMessage.AutoSize = true;
            this.WarningMessage.Location = new System.Drawing.Point(73, 27);
            this.WarningMessage.Margin = new System.Windows.Forms.Padding(3);
            this.WarningMessage.MaximumSize = new System.Drawing.Size(620, 0);
            this.WarningMessage.Name = "WarningMessage";
            this.WarningMessage.Size = new System.Drawing.Size(75, 13);
            this.WarningMessage.TabIndex = 7;
            this.WarningMessage.Text = "%MESSAGE%";
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(531, 127);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 22);
            this.ContinueButton.TabIndex = 5;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // SkipButton
            // 
            this.SkipButton.Location = new System.Drawing.Point(617, 127);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.Size = new System.Drawing.Size(75, 22);
            this.SkipButton.TabIndex = 6;
            this.SkipButton.Text = "Skip";
            this.SkipButton.UseVisualStyleBackColor = true;
            this.SkipButton.Click += new System.EventHandler(this.SkipButton_Click);
            // 
            // WarningDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 161);
            this.ControlBox = false;
            this.Controls.Add(this.DisplayImage);
            this.Controls.Add(this.WarningMessage);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.SkipButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WarningDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yo, hold up";
            ((System.ComponentModel.ISupportInitialize)(this.DisplayImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DisplayImage;
        private System.Windows.Forms.Label WarningMessage;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button SkipButton;
    }
}