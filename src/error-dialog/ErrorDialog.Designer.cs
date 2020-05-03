﻿namespace NeoDefaults_Installer { 
    partial class ErrorDialog {
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
            this.quitButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.errorMessage = new System.Windows.Forms.Label();
            this.errorImage = new System.Windows.Forms.PictureBox();
            this.CopyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorImage)).BeginInit();
            this.SuspendLayout();
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(347, 129);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 22);
            this.quitButton.TabIndex = 1;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(261, 129);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 22);
            this.ContinueButton.TabIndex = 1;
            this.ContinueButton.Text = "Okay";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Critical Error";
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(73, 46);
            this.errorMessage.Margin = new System.Windows.Forms.Padding(3);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(75, 13);
            this.errorMessage.TabIndex = 2;
            this.errorMessage.Text = "%MESSAGE%";
            // 
            // errorImage
            // 
            this.errorImage.Location = new System.Drawing.Point(21, 27);
            this.errorImage.Name = "errorImage";
            this.errorImage.Size = new System.Drawing.Size(50, 50);
            this.errorImage.TabIndex = 3;
            this.errorImage.TabStop = false;
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(12, 127);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(136, 22);
            this.CopyButton.TabIndex = 1;
            this.CopyButton.Text = "Copy text to clipboard";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // ErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 161);
            this.ControlBox = false;
            this.Controls.Add(this.errorImage);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.quitButton);
            this.Name = "ErrorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oh, poop";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.errorImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label errorMessage;
        private System.Windows.Forms.PictureBox errorImage;
        private System.Windows.Forms.Button CopyButton;
    }
}