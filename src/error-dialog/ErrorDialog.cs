using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NeoDefaults_Installer {
    public partial class ErrorDialog : Form {
        public ErrorDialog() {
            InitializeComponent();
            errorImage.Image = SystemIcons.Error.ToBitmap();
        }

        public void DisplayError(String message) {
            errorMessage.Text = message;
            this.ShowDialog();
        }

        private void quitButton_Click(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        // Unimplemented methods— take no action if these events are triggered
        private void label2_Click(object sender, EventArgs e) {}
        private void errorImage_Click(object sender, EventArgs e) {}
        private void errorMessage_Click(object sender, EventArgs e) {}

        private void continueButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }
    }
}
