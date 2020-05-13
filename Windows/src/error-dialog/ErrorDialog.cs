using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NeoDefaults_Installer {
    public partial class ErrorDialog : Form {
        private readonly int MAX_MESSAGE_LENGTH = 750;
        private String message = "%MESSAGE%";
        public ErrorDialog() {
            InitializeComponent();
            errorImage.Image = SystemIcons.Error.ToBitmap();
            this.TopMost = true;
        }

        public void DisplayAndExit(String message) {
            this.message = message;
            String displayMessage = message;
            if (message.Length > MAX_MESSAGE_LENGTH) {
                displayMessage = displayMessage.Substring(0, MAX_MESSAGE_LENGTH);
                displayMessage += "...";
            }
            ErrorMessage.Text = displayMessage;

            var result = this.ShowDialog();
            Environment.Exit(1);
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
