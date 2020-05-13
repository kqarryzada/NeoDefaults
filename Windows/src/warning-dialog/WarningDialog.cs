using System;
using System.Drawing;
using System.Windows.Forms;

namespace NeoDefaults_Installer.warning_dialog {
    public partial class WarningDialog : Form {
        private readonly int MAX_MESSAGE_LENGTH = 750;

        public WarningDialog() {
            InitializeComponent();
            DisplayImage.Image = SystemIcons.Warning.ToBitmap();
            this.TopMost = true;
        }

        public DialogResult Display(String message) {
            if (message.Length > MAX_MESSAGE_LENGTH) {
                message = message.Substring(0, MAX_MESSAGE_LENGTH);
                message += "...";
            }
            WarningMessage.Text = message;
            var result = this.ShowDialog();
            return result;
        }

        private void ContinueButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SkipButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
