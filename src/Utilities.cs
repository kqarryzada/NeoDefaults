using System;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeoDefaults_Installer {
    /**
     * This class stores helpful methods that are unrelated to UI elements.
     */
    public class Utilities {
        // Stores the location of the folder containing the this tool (e.g., the .exe file) on the
        // user's machine.
        private readonly String basePath;

        // Stores the location of the "Team Fortress 2" folder on the user's machine. If unknown,
        // this will be null.
        public String tfPath = null;

        // TF-path related parameters. You must use String.Format() and specify a drive name to use these.
        private readonly String[] defaultInstallLocations = {
            @"{0}Program Files (x86)\Steam\SteamApps\common\Team Fortress 2\hl2.exe",
            @"{0}Steam\SteamApps\common\Team Fortress 2\hl2.exe",
            @"{0}SteamLibrary\SteamApps\common\Team Fortress 2\hl2.exe",
        };

        // The filename for the source autoexec file that came with the installer.
        private readonly String autoexecSourceName = "autoexec-alpha.cfg";

        // The filename of the destination config file.
        private readonly String cfgDestName = "neodefaults.cfg";

        private static readonly Utilities singleton = new Utilities();

        private Logger log = Logger.GetInstance();

        private Utilities() {
            // When developing, the base filepath is two parent directories above the
            // executable.
            String parentPath = (Main.DEVELOP_MODE) ? @"..\.." : @".";

            String relativeBasePath = AppDomain.CurrentDomain.BaseDirectory;
            relativeBasePath = Path.Combine(relativeBasePath, parentPath);
            basePath = new FileInfo(relativeBasePath).FullName;

            // On startup, try to determine the path to the TF2 installation.
            SearchForTF2Install();
        }

        public static Utilities GetInstance() {
            return singleton;
        }

        /**
         * Tries to find a TF2 installation in the most common locations.
         */
        private async void SearchForTF2Install() {
            // Obtain the list of drive names on the system.
            DriveInfo[] systemDrives = null;
            await Task.Run(() => {
                try {
                    systemDrives = DriveInfo.GetDrives();
                }
                catch (Exception e) {
                    log.WriteErr("An issue occurred in trying to obtain the list of drives on the machine.", 
                                    e.ToString());

                    // It should be safe to at least check the C: drive before completely bailing
                    DriveInfo[] c = new DriveInfo[1];
                    c[0] = new DriveInfo("C");
                    systemDrives = c;
                }
            });
            // Will hold the location to a TF2 install, if not null.
            String hl2Path = null;

            // Placeholder for paths being checked. Allocated here for use by debug messages.
            String path = null;

            // Search each common installation path for files under all drives on the system.
            await Task.Run(() => {
                try {
                    foreach (DriveInfo drive in systemDrives) {
                        log.Write();
                        foreach (String _path in defaultInstallLocations) {
                            path = String.Format(_path, drive.Name);

                            log.Write("Checking if the path exists: " + path);
                            if (File.Exists(path)) {
                                hl2Path = path;
                                log.Write();
                                log.Write("Found install at: " + path);

                                // It's a nested loop. Stop being judgemental.
                                goto EndOfLoop;
                            }
                        }
                    }
                }
                catch (FormatException f) {
                    path = (path == null) ? "<null>" : path;
                    log.WriteErr("An issue occurred when trying to format the name of a drive"
                                 + " into '" + path + "'.",
                                 f.ToString());
                }
                catch (ArgumentNullException a) {
                    log.WriteErr("A null pointer was found when trying to format the name of a"
                                 + " drive. The provided drive was likely null.",
                                 a.ToString());
                }

            EndOfLoop:;
            });

            if (hl2Path != null) {
                // Strip "hl2.exe" from the path to obtain the folder path
                tfPath = CanonicalizePath(Path.GetDirectoryName(hl2Path));
            }
        }

        /**
         * Returns the canonicalized filepath for 'path'.
         */
        public String CanonicalizePath(String path) {
            log.Write("Attempting to canonicalize the path of: " + path);
            String testPath = null;

            try {
                testPath = Path.GetFullPath(path);
            }
            catch (PathTooLongException p) {
                log.WriteErr("Tried to set the path of the TF2 install, but it was too long."
                             + " The provided path was:\n\r",
                             path,
                             p.ToString());
            }
            catch (SecurityException s) {
                log.WriteErr("Did not have permission to obtain the canonical path of '" + path
                             + "'. Aborting.",
                             s.ToString());
            }
            catch (Exception e) {
                path = (path == null) ? "<null>" : path;
                log.WriteErr("Could not obtain the canonical path of '" + path + "'. Aborting.",
                            e.ToString());
            }

            if (testPath != null) {
                log.Write("Path was found to be: " + testPath);
                log.Write();
            }
            return testPath;
        }

        /**
         * Copies a 'sourceFile' to a destination. The filepath of the destination is given by
         * 'destFile', which includes the filename of the file (allowing for a move-and-rename
         * operation to be executed at once). This method will give a few attempts at copying the file
         * over before reporting an issue.
         * 
         * sourceFile: The file to be copied.
         * destFile: The resulting file after the copy is complete.
         * overwrite: If true, overwrite the existing file. If not, simply skip the operation.
         *
         * Returns false if the expected file was not created.
         */
        private void CopyFile(String sourceFile, String destFile, bool overwrite) {
            int numRetries = 3;

            // Infuriatingly enough, File.Copy() will throw an IOException both in cases of I/O
            // issues and if the destination file already exists. To be certain, first check for the
            // existence of the resulting file. There are currently no scenarios where the user
            // would need to be immediately notified of this.
            if (!overwrite && File.Exists(destFile)) {
                var msg = String.Format("Tried to create '{0}' from '{1}' because the resulting"
                                        + "file already exists, and overwrite was not permitted.",
                                        destFile, sourceFile);
                log.WriteErr(msg);
                return;
            }

            // Allow a few retry attempts in case of transient issues.
            for (int i = 0; i < numRetries; i++) {
                try {
                    // Attempt a copy. If it is successful, leave the loop. Otherwise, the exception
                    // is caught.
                    File.Copy(sourceFile, destFile, overwrite);
                    break;
                }
                catch (Exception) when (i < numRetries - 1) {
                    Thread.Sleep(500);
                }
                catch (Exception e) {
                    var logMsg = String.Format("A problem occurred when trying to create '{0}' from '{1}'.",
                                                    destFile, sourceFile);
                    log.WriteErr(logMsg, e.ToString());


                    // Form dlg1 = new Form();
                    // dlg1.ShowDialog();
                    Environment.Exit(1);
                }
            }
        }

        /**
         * Extracts a zip file to a folder.
         * 
         * 'fileName' is a given nickname for the resulting zip file, and is only used iin the
         * error message if an issue occurs.
         */
        private void ExtractZip(String zipFilepath, String destinationFolder, String fileName) {
            try {
                ZipFile.ExtractToDirectory(zipFilepath, destinationFolder);
            }
            catch (IOException) {
                String msg = String.Format("An error occurred when trying to install '{0}' to '{1}'"
                                           + ". Do you already have this installed?", fileName, destinationFolder);
                // TODO: Display this message to the user
                log.Write(msg);
            }
        }

        /**
         * In order for neodefaults.cfg to be run when TF2 is launched, autoexec.cfg must
         * execute the file. This method adds the needed lines to the autoexec file in order
         * to accomplish this. If the autoexec.cfg file does not exist, it will be created.
         *
         * neodefaultsPath: The path to the newly-installed neodefaults.cfg file
         *
         * Throws an Exception if an unexpected error occurs.
         */
        private void AppendLinesToAutoExec(String neodefaultsPath) {
            String autoexec;
            String defaultLocation = Path.Combine(tfPath, @"tf\cfg\", "autoexec.cfg");
            String mastercomfigLocation = Path.Combine(tfPath, @"tf\cfg\user", "autoexec.cfg");

            // Check for a Mastercomfig (https://mastercomfig.com/) install. This is a popular
            // plugin used by many players, and it expects autoexec.cfg to be stored in cfg/user/
            // instead of the usual cfg/ directory. In order to fully support these users, the
            // required execution lines must be added to the correct file.
            String[] filePaths = Directory.GetFiles(Path.Combine(tfPath, @"tf\custom"),
                                                    "mastercomfig*preset.vpk",
                                                    SearchOption.TopDirectoryOnly);
            autoexec = (filePaths.Length == 0) ? defaultLocation : mastercomfigLocation;

            // File has been found, append lines.
            StringBuilder sb = new StringBuilder();
            if (File.Exists(autoexec)) {
                sb.Append(Environment.NewLine);
            }
            sb.Append("//--------Added by the NeoDefaults Installer--------//");
            sb.Append(Environment.NewLine);
            sb.Append("exec ");
            sb.Append(Path.GetFileNameWithoutExtension(neodefaultsPath));
            sb.Append(Environment.NewLine);
            sb.Append("//--------------------------------------------------//");
            sb.Append(Environment.NewLine);

            File.AppendAllText(autoexec, sb.ToString());
        }

        /**
         * Installs idHUD in the custom/ directory.
         */
        public async Task InstallHUD() {
            await Task.Run(() => {
                String zipFilepath = Path.Combine(basePath, @"custom-files\idhud-master.zip");
                String destination = Path.Combine(tfPath, @"tf\custom");
                var logMsg = String.Format("Installing HUD from '{0}' to '{1}'.", zipFilepath, destination);
                log.Write(logMsg);

                ExtractZip(zipFilepath, destination, "Improved Default HUD");
                log.Write("HUD installation complete.");
            });
        }

        /**
         * In order for idhud to work properly, some fonts need to be installed, which are provided
         * in idhud's zip file. This method installs the fonts on the user's machine.
         */
        public async Task InstallHUDFonts() {
            await Task.Run(() => {
                String fontsPath = Path.Combine(tfPath, @"tf\custom\idhud-master\resource\fonts");
                String windowsFontsPath = Path.Combine(Environment.GetEnvironmentVariable("windir"), "Fonts");

                // Copy each file over to the windows fonts directory to install them.
                foreach (string font in Directory.GetFiles(fontsPath)) {
                    String destFile = Path.Combine(windowsFontsPath, Path.GetFileName(font));
                    var logMsg = String.Format("Installing '{0}' font to '{1}'.", fontsPath, destFile);
                    log.Write(logMsg);

                    CopyFile(font, destFile, false);
                }

                log.Write("Font installation complete.");
            });
        }

        /**
         * Installs the custom hitsound.
         */
        public async Task InstallHitsound() {
            await Task.Run(() => {
                String hitsoundZip = Path.Combine(basePath, @"custom-files\neodeafults-hitsound.zip");
                String destination = Path.Combine(tfPath, @"tf\custom");
                var logMsg = String.Format("Installing hitsound from '{0}' to '{1}'.", hitsoundZip, destination);
                log.Write(logMsg);

                ExtractZip(hitsoundZip, destination, "Custom Quake hitsound");
                log.Write("Hitsound installation complete.");
            });
        }

        /**
         * Installs the neoDefaults.cfg file.
         */
        public async Task InstallConfig() {
            await Task.Run(() => {
                String sourceConfig = Path.Combine(basePath, @"custom-files\", autoexecSourceName);
                String destConfig = Path.Combine(tfPath, @"tf\cfg\", cfgDestName);
                var logMsg = String.Format("Installing config file from '{0}' to '{1}'.", sourceConfig, destConfig);
                log.Write(logMsg);

                try {
                    CopyFile(sourceConfig, destConfig, true);
                    AppendLinesToAutoExec(destConfig);
                }
                catch (Exception e) {
                    log.WriteErr("An error occurred when trying to install the config files.",
                                 e.ToString());
                    return;
                }

                log.Write("Config installation complete.");
            });
        }
    }
}
