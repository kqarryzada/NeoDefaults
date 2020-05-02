using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NeoDefaults_Installer {
    public partial class ErrorDialog : Form {
        private String Message = "%MESSAGE%";
        public ErrorDialog() {
            InitializeComponent();
            errorImage.Image = SystemIcons.Error.ToBitmap();
        }

        public void DisplayError(String msg) {
            Message = msg;
            errorMessage.Text = Message;
            this.ShowDialog();
        }

        private void quitButton_Click(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        private void ContinueButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void CopyButton_Click(object sender, EventArgs e) {
            Clipboard.SetText(Message);
        }
    }
}
