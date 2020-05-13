using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NeoDefaults_Installer {

    /**
     * This class is responsible for handling writes to the log file.
     *
     * In the unlikely event that the log file fails to initialize on program startup, there's
     * really not much that can be done. While it's a huge problem, especially if the log is needed
     * to be viewed after the program has run, it's really not worth crashing the entire program
     * just because of the log file. If there's a serious underlying problem, it would likely be
     * exposed later during the program's run.
     *
     * Unless a method explicitly states that it can throw an exception, all methods in this class
     * will be silent upon a failure and return without completing the requested task.
     */
    public class Logger {
        private static readonly Logger singleton = new Logger();

        // The reference to the file being logged.
        private readonly FileStream logFile;

        // The name of the folder that will hold the log files.
        private readonly String BASE_FOLDER_NAME = "NeoDefaults";

        // String divider used for a visual barrier for messages in the log file.
        private readonly String DIVIDER =
            "---------------------------------------------------------------------------------\n\r";


        private Logger() {
            String commonAppData = Environment.GetFolderPath(
                                        Environment.SpecialFolder.CommonApplicationData);

            String logFilePath = Path.Combine(commonAppData, BASE_FOLDER_NAME);
            DirectoryInfo dir = Directory.CreateDirectory(logFilePath);

            String fileName = Path.Combine(logFilePath, "log.txt");
            String fileNamePrev = Path.Combine(logFilePath, "log_prev.txt");
            if (File.Exists(fileNamePrev)) {
                // Rotate the existing file to an old one. Only keep one previous record.
                File.Delete(fileNamePrev);
            }
            if (File.Exists(fileName)) {
                File.Move(fileName, fileNamePrev);
            }
            logFile = File.Create(fileName);
            logFile.Close();

            InitializeLogFile();
        }


        /**
         *  Initializes the log file upon the beginning of the program's run. The 'logFile' member
         *  variable must be initialized before this method is called.
         *
         *  Throws an ArgumentNullException if the logfile is null.
         */
        private void InitializeLogFile() {
            if (logFile.Name == null) {
                throw new ArgumentNullException();
            }

            StringBuilder sb = new StringBuilder();
            try {
                sb.Append("Logfile initialized on: ");
                sb.AppendLine(DateTime.Now.ToString());
                sb.Append("Version ");
                sb.AppendLine(Main.PRODUCT_VERSION);
                sb.AppendLine();

                File.WriteAllText(logFile.Name, sb.ToString());
            }
            catch (Exception e) {
                Debug.Print("Failed to initialize log file:");
                Debug.Print(e.ToString());
            }
        }


        public static Logger GetInstance() {
            return singleton;
        }


        /**
         * Prints the divider in the log file to help visually establish a certain area. If the
         * logfile is uninitialized, the write attempt will be ignored.
         */
        public void PrintDivider() {
            if (logFile == null)
                return;

            try {
                File.AppendAllText(logFile.Name, DIVIDER);
            }
            catch (Exception e) {
                Debug.Print("Failed to print the DIVIDER characters.");
                Debug.Print(e.ToString());
            }
        }

        /**
         * Writes the specified text to the log. If the logfile is uninitialized, the write attempt
         * will be ignored.
         */
        public void Write(params String[] logLines) {
            if (logFile == null)
                return;

            // If called with no parameters, just print a newline.
            if (logLines.Length == 0)
                logLines = new[] { "" };

            try {
                foreach (String s in logLines) {
                    File.AppendAllText(logFile.Name, s + Environment.NewLine);
                    if (Main.DEVELOP_MODE)
                        Debug.Print("Log: " + s);
                }
            }
            catch (Exception e) {
                Debug.Print("Failed to write the requested message.");
                Debug.Print(e.ToString());
            }
        }


        /**
         * Writes the error message to the log, and surrounds it with a divider to make it very
         * apparent that an error happened. If the logfile is uninitialized, the write attempt will
         * be ignored.
         */
        public void WriteErr(params String[] logLines) {
            if (logFile == null)
                return;

            try {
                File.AppendAllText(logFile.Name, DIVIDER);
                File.AppendAllText(logFile.Name, "Error: ");
                Write(logLines);
                File.AppendAllText(logFile.Name, DIVIDER);
            }
            catch (Exception e) {
                Debug.Print("Failed to write an error message.");
                Debug.Print(e.ToString());
            }
        }
    }
}
