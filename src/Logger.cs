using System;
using System.Diagnostics;
using System.IO;

namespace NeoDefaults_Installer {
    public class Logger {
        private static readonly Logger singleton = new Logger();

        // The reference to the file being logged.
        private readonly FileStream logFile;

        // The name of the folder that will hold the log files.
        private readonly String BASE_FOLDER_NAME = "NeoDefaults";

        // String divider used for a visual barrier for error messages in the log file.
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

            var firstLine = String.Format("Logfile initialized on {0}.", DateTime.Now) 
                                + Environment.NewLine;
            File.WriteAllText(fileName, firstLine);
        }


        public static Logger GetInstance() {
            return singleton;
        }

        /**
         * Writes the specified text to the log.
         *
         * If an error occurs in writing to the log file, this method will silently fail.
         */
        public void Write(params String[] logLines) {
            // If called with no parameters, just print a newline.
            if (logLines.Length == 0)
                logLines = new[] { "" } ;

            try {
                foreach (String s in logLines) {
                    File.AppendAllText(logFile.Name, s + Environment.NewLine);
                    if (Main.DEVELOP_MODE)
                        Debug.Print("Log: " + s);
                }
            }
            catch (Exception e) {
                // If the write fails, there's really not much that can be done.
                Debug.Print("Failed to write the requested message.");
                Debug.Print(e.ToString());
            }
        }


        /**
         * Writes the error message to the log, and surrounds it with a divider to make it very
         * apparent that an error happened.
         *
         * If an error occurs in writing to the log file, this method will silently fail.
         */
        public void WriteErr(params String[] logLines) {
            try {
                File.AppendAllText(logFile.Name, DIVIDER);
                File.AppendAllText(logFile.Name, "Error: ");
                Write(logLines);
                File.AppendAllText(logFile.Name, DIVIDER);
            }
            catch (Exception e) {
                // If the write fails, there's really not much that can be done.
                Debug.Print("Failed to write an error message.");
                Debug.Print(e.ToString());
            }
        }
    }
}
