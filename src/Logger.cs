using System;
using System.Diagnostics;
using System.IO;

namespace NeoDefaults_Installer {
    public class Logger {
        private static readonly Logger singleton = new Logger();

        private readonly FileStream logFile;

        private readonly String BASE_FOLDER_NAME = "NeoDefaults";

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
            File.WriteAllText(fileName, "Logfile initialized.\n\r\n\r");
        }

        public static Logger GetInstance() {
            return singleton;
        }

        public void Write(params String[] logLines) {
            foreach (String s in logLines) {
                File.AppendAllText(logFile.Name, s + Environment.NewLine);
                if (Main.DEVELOP_MODE)
                    Debug.Print("Log: " + s);
            }
        }
    }
}
