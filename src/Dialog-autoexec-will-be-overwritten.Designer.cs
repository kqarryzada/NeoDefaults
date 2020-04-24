namespace NeoDefaults_Installer { 
    partial class Dialog_autoexec_will_be_overwritten {
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
            this.continueAnywayButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(360, 101);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(87, 23);
            this.quitButton.TabIndex = 1;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            // 
            // continueAnywayButton
            // 
            this.continueAnywayButton.Location = new System.Drawing.Point(238, 101);
            this.continueAnywayButton.Name = "continueAnywayButton";
            this.continueAnywayButton.Size = new System.Drawing.Size(116, 23);
            this.continueAnywayButton.TabIndex = 1;
            this.continueAnywayButton.Text = "Continue anyway";
            this.continueAnywayButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "An existing \"autoexec.cfg\" file was detected.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(398, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "If you continue, the existing file will be backed up to \\tf\\cfg\\NeoDefaults\\.";
            // 
            // Dialog_autoexec_will_be_overwritten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 136);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.continueAnywayButton);
            this.Controls.Add(this.quitButton);
            this.Name = "Dialog_autoexec_will_be_overwritten";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Continue?";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Button continueAnywayButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
