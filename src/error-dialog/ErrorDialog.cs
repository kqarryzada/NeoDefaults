using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NeoDefaults_Installer {
    public partial class ErrorDialog : Form {
        private String message = "%MESSAGE%";
        public ErrorDialog() {
            InitializeComponent();
            errorImage.Image = SystemIcons.Error.ToBitmap();
        }

        public DialogResult Display(String message) {
            this.message = message;
            errorMessage.Text = message;
            var result = this.ShowDialog();
            return result;
        }

        private void QuitButton_Click(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        private void ContinueButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CopyButton_Click(object sender, EventArgs e) {
            // The clipboard can only be accessed on an STAThread.
            Thread thread = new Thread(() => Clipboard.SetText(message));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
