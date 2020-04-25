using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace NeoDefaults_Installer {
    public class Logger {
        private static readonly Logger singleton = new Logger();

        private readonly FileStream logFile;

        private readonly String BASE_FOLDER_NAME = "NeoDefaults";

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

        private void Print(String s) {
            File.AppendAllText(logFile.Name, s + Environment.NewLine);
            if (Main.DEVELOP_MODE)
                Debug.Print("Log: " + s);
        }

        public void Write(params String[] logLines) {
            // If called with no parameters, just print a newline.
            if (logLines.Length == 0)
                logLines = new[] { "" } ;

            foreach (String s in logLines) {
                File.AppendAllText(logFile.Name, s + Environment.NewLine);
                if (Main.DEVELOP_MODE)
                    Debug.Print("Log: " + s);
            }
        }

        public void WriteErr(params String[] logLines) {
            File.AppendAllText(logFile.Name, DIVIDER);
            File.AppendAllText(logFile.Name, "Error: ");
            Write(logLines);
            File.AppendAllText(logFile.Name, DIVIDER);
        }
    }
}
