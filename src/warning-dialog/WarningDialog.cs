using System;
using System.Drawing;
using System.Windows.Forms;

namespace NeoDefaults_Installer.warning_dialog {
    public partial class WarningDialog : Form {
        public WarningDialog() {
            InitializeComponent();
            DisplayImage.Image = SystemIcons.Warning.ToBitmap();
            this.TopMost = true;
        }

        public DialogResult Display(String message) {
            errorMessage.Text = message;
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
