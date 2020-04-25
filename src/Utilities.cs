using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

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

        // The filename of the destination autoexec file. When officially released, this is set to
        // "autoexec.cfg".
        private readonly String autoexecDestName;

        private static readonly Utilities singleton = new Utilities();

        private Utilities() {
            // When developing, the base filepath is two parent directories above the
            // executable.
            String parentPath;
            if (Main.DEVELOP_MODE) {
                parentPath = @"..\..";
                autoexecDestName = "autoexec-TEST.cfg";
            }
            else {
                parentPath = @".";
                autoexecDestName = "autoexec.cfg";
            }

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
                    Debug.WriteLine("An issue occurred in trying to obtain the list of drives on the machine.");
                    Debug.Write(e.ToString());

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
                        Debug.WriteLine("");
                        foreach (String _path in defaultInstallLocations) {
                            path = String.Format(_path, drive.Name);

                            Debug.Print("Looking for install in: " + path);
                            if (File.Exists(path)) {
                                hl2Path = path;
                                Debug.Print("\n\rFound install at: " + path);
                            }
                        }
                    }
                }
                catch (FormatException f) {
                    path = (path == null) ? "<null>" : path;
                    Debug.WriteLine("\n\rAn issue occurred when trying to format the name of a drive"
                                    + "into '" + path + "'.");
                    Debug.WriteLine(f.ToString());
                }
                catch (ArgumentNullException a) {
                    Debug.WriteLine("\n\rA null pointer was found when trying to format the name of a "
                                    + "drive. The provided drive was likely null.");
                    Debug.WriteLine(a.ToString());
                }
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
            String testPath = null;

            try {
                testPath = Path.GetFullPath(path);
            }
            catch (PathTooLongException p) {
                Debug.WriteLine("Tried to set the path of the TF2 install, but it was too long. The provided path was:\n\r");
                Debug.WriteLine(path);
                Debug.WriteLine(p.ToString());
            }
            catch (SecurityException s) {
                Debug.WriteLine("Did not have permission to obtain the canonical path of '" + path + "'. Aborting.");
                Debug.WriteLine(s.ToString());
            }
            catch (Exception e) {
                path = (path == null) ? "<null>" : path;
                Debug.WriteLine("\n\rCould not obtain the canonical path of '" + path + "'. Aborting.");
                Debug.WriteLine(e.ToString());
            }

            return testPath;
        }

        private void CopyFile(String sourceFile, String destFile) {
            CopyFile(sourceFile, destFile, false);
        }

        /**
         * Copies a 'sourceFile' to a destination. The filepath of the destination is given by 
         * 'destFile', which includes the filename of the file (allowing for a move-and-rename
         * operation to be executed at once). This method will give a few attempts at copying the file
         * over before reporting an issue.
         */
        private void CopyFile(String sourceFile, String destFile, bool overwrite) {
            int NUM_RETRIES = 3;

            // If 'destFile' already exists before the copy process, notify the user and ask if they
            // want to continue.
            if (!overwrite && File.Exists(destFile)) {
                // Form dlg1 = new Form();
                // dlg1.ShowDialog();
                Debug.Print("The destination file '" + destFile + "' already exists.");

                // pseudo-code:
                // switch (response) {
                //     case overwrite_ok:
                //         overwrite = true;
                //         break;
                //     case skip:
                //         // The file is no longer needed to be copied over, so leave.
                //         return;
                //     case backup:
                //         // do backup
                //          break;
                // }
            }

            // Allow a few retry attempts in case of transient issues.
            for (int i = 0; i < NUM_RETRIES; i++) {
                try {
                    // Attempt a copy. If it is successful, leave the loop. Otherwise, the exception
                    // is caught.
                    File.Copy(sourceFile, destFile, overwrite);
                    break;
                }
                catch (IOException) when (i < NUM_RETRIES - 1) {
                    Thread.Sleep(1000);
                }
                catch (IOException ioe) {
                    Debug.Print("A problem occurred when trying to create '" + destFile + "' from '" + sourceFile + "'.");
                    Debug.Print(ioe.ToString());
                    // Environment.Exit(1);
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
                Debug.Print("An error occurred when trying to install '" + fileName + "' to '"
                            + destinationFolder + "'. Do you already have this installed?");
            }
        }

        /**
         * Installs idHUD in the custom/ directory.
         */
        public async Task InstallHUD() {
            String zipFilepath = Path.Combine(basePath, @"custom-files\idhud-master.zip");
            String destination = Path.Combine(tfPath, @"tf\custom");
            await Task.Run(() => {
                ExtractZip(zipFilepath, destination, "Improved Default HUD");
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
                    CopyFile(font, destFile);
                }
            });
        }

        /**
         * Installs the custom hitsound.
         */
        public async Task InstallHitsound() {
            await Task.Run(() => {
                String hitsoundZip = Path.Combine(basePath, @"custom-files\neodeafults-hitsound.zip");
                String destination = Path.Combine(tfPath, @"tf\custom");
                ExtractZip(hitsoundZip, destination, "Custom Quake hitsound");
            });
        }

        /**
         * Installs autoexec.cfg.
         */
        public async Task InstallConfig() {
            await Task.Run(() => {
                String sourceAutoexec = Path.Combine(basePath, @"custom-files\", autoexecSourceName);
                String destAutoexec = Path.Combine(tfPath, @"tf\cfg\", autoexecDestName);
                CopyFile(sourceAutoexec, destAutoexec);
            });
        }
    }
}
