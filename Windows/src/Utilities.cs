using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NeoDefaults_Installer.warning_dialog;

namespace NeoDefaults_Installer {
    /**
     * This class stores helpful methods that are unrelated to UI elements.
     */
    public class Utilities {
        // Stores the parent location of the files that are to be installed (e.g., the source cfg
        // file, the hitsound file, etc.).
        private readonly String componentsPath;

        // Stores the location of the "Team Fortress 2/tf/" folder on the user's machine. If 
        // unknown, this will be null.
        public String tfPath = null;

        // TF-path related parameters. You must use String.Format() and specify a drive name to use these.
        private readonly String[] defaultInstallLocations = {
            @"{0}Program Files (x86)\Steam\SteamApps\common\Team Fortress 2\tf",
            @"{0}Steam\SteamApps\common\Team Fortress 2\tf",
            @"{0}SteamLibrary\SteamApps\common\Team Fortress 2\tf",
        };

        // The name of the folder inside of tf/cfg/ that holds the installer config files.
        private readonly String configFolderName = "NeoDefaults";

        // The name of the config file to be installed.
        private readonly String sourceCfgName = "NeoDefaults-v" + Main.PRODUCT_VERSION + ".cfg";

        // The name of the config file once it has been installed on the user's machine.
        private readonly String destCfgName = "neodefaults.cfg";

        // The name of the custom config file, which allows a user to override any values that are
        // set by the NeoDefaults config.
        private readonly String customCfgName = "custom.cfg";

        private static readonly Utilities singleton = new Utilities();

        private readonly Logger log = Logger.GetInstance();

        // An installation of TF2 should take up around 21 GB of space. Thus, if a drive on the
        // filesystem is less than 16 GB, don't bother searching it for a TF2 install.
        private readonly long MIN_DRIVE_SIZE = 16 * ((long) 1 << 30);

        // The header and footer to surround the "exec" statement that gets added to autoexec.cfg.
        private readonly String AutoexecHeader =
                                        "//--------Added by the NeoDefaults Installer--------//";

        private readonly String AutoexecFooter =
                                        "//--------------------------------------------------//";

        // Names of the VPK files to be installed
        private readonly String HudVpkName = "neodefaults-hud-tweaks.vpk";
        private readonly String HitVpkName = "neodefaults-quake-hitsound.vpk";



        // Return codes for installations. These help report whether an install failed, 
        // succeeded, etc.
        public enum InstallStatus {
            FAIL,
            SUCCESS,
            OPT_OUT,
        };

        private Utilities() {
            // When released, the components are stored in the "resource/" directory alongside the
            // program.
            String relativePath = (Main.DEVELOP_MODE) ? @"..\..\..\build" : "resource";
            try {
                componentsPath = Path.GetFullPath(
                                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));
            }
            catch (Exception e) {
                log.WriteErr("Failed to initialize the resource path.", e.ToString());
                throw e;
            }

            if (!Directory.Exists(componentsPath)) {
                String msg = "Could not find the path to '" + componentsPath + "'.";
                if (Main.DEVELOP_MODE) {
                    msg += " Have you run the build.sh script?";
                }
                else {
                    msg += " Was the folder deleted?";
                }
                log.WriteErr(msg);
                var dialog = new ErrorDialog();
                dialog.DisplayAndExit(msg);
            }

            // On startup, try to determine the path to the TF2 installation on the machine.
            SearchForTF2Install();
        }

        public static Utilities GetInstance() {
            return singleton;
        }


        /**
         * Searches for a TF2 installation in the most common locations on the machine.
         *
         * This method obtains all the drives available on the system, then searches for a TF2 path
         * under each drive until either a valid install is found, or until all possibilities have
         * been exhausted.
         *
         * If a potential install is found, it will be stored in the 'tfPath' private member
         * variable, but will not officially be recognized until it is handled in Main (specifically
         * in Main.PreparePathPanel()) since there are UI elements that depend on whether or not the
         * install was automatically found.
         */
        private async void SearchForTF2Install() {
            log.PrintDivider();
            log.Write("Beginning automatic filepath check...");

            // Before initiating an expensive search, make use of the SpecialFolder enum to look in
            // the default installation path. This will find the correct drive letter (e.g., 'C:\')
            // for us.
            try {
                var progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                var defaultPath = Path.Combine(progFiles + @"\Steam\SteamApps\common\Team Fortress 2\tf");
                if (Directory.Exists(defaultPath)) {
                    log.Write("Found the path in the default location, '" + defaultPath + "'.");
                    log.PrintDivider();
                    tfPath = defaultPath;
                    return;
                }
            }
            catch (Exception e) {
                log.WriteErr("Failed to perform the check on the default path.",
                                e.ToString());
            }


            log.WriteLn("The install was not found in the default location. Preparing to perform"
                        + " a search of the drives.");
            // Obtain the list of all drive names on the system.
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
            String installPath = null;

            // Placeholder for paths being checked. Allocated here for use by debug messages.
            String path = null;

            // Search each common installation path for files under all drives on the system.
            await Task.Run(() => {
                try {
                    foreach (DriveInfo drive in systemDrives) {
                        // If a drive is too small to hold TF2, don't bother searching it.
                        long size = drive.TotalSize;
                        if (size <= MIN_DRIVE_SIZE) {
                            log.WriteLn("Skipping over the " + drive.Name + " drive since it has size '"
                                        + size + "', which is less than the threshold of "
                                        + MIN_DRIVE_SIZE + " bytes.");
                            continue;
                        }

                        foreach (String _path in defaultInstallLocations) {
                            path = String.Format(_path, drive.Name);

                            log.Write("Checking if the path exists: " + path);
                            if (Directory.Exists(path)) {
                                installPath = path;
                                log.Write(Environment.NewLine + "Found install at: " + path);

                                // It's a nested loop. Stop being judgemental.
                                goto EndOfLoop;
                            }
                        }
                        log.Write();
                    }

                EndOfLoop:
                    log.PrintDivider();
                    log.Write();
                }
                catch (Exception e) {
                    log.WriteErr("An error occurred when trying to search the system for a TF2 install:",
                                    e.ToString());
                }
            });

            if (installPath != null) {
                try {
                    String parent = Path.GetDirectoryName(installPath);
                    tfPath = Path.Combine(parent, "tf");
                }
                catch (Exception e) {
                    log.WriteErr("Failed to set the TF2 install path from '" + installPath + "'.",
                                    e.ToString());
                }
            }
        }


        /**
         * Returns the canonicalized filepath for 'path', or 'null' if there was a problem.
         */
        public String CanonicalizePath(String path) {
            log.Write("Attempting to canonicalize the path of: " + path);
            if (path == null) {
                return null;
            }

            String testPath = null;
            try {
                testPath = Path.GetFullPath(path);
            }
            catch (Exception e) {
                log.WriteErr("Could not obtain the canonical path of '" + path + "'. Aborting.",
                            e.ToString());
            }

            if (testPath != null) {
                log.WriteLn("The path was found to be: " + testPath);
            }
            return testPath;
        }


        /**
         * Copies a 'sourceFile' to a destination. The filepath of the destination is given by
         * 'destFile', which includes the filename of the file (allowing for a move-and-rename
         * operation to be executed at once). This method will give a few attempts at copying the file
         * over before reporting an issue.
         * 
         * sourceFile: The full path to the file that is to be copied.
         * destFile:   The full path to the resulting file after the copy is complete.
         * overwrite:  If true, and the file already exists, the existing file will be overwritten.
         *
         * Throws an Exception if an unexpected error occurs.
         */
        private void CopyFile(String sourceFile, String destFile, bool overwrite) {
            int numRetries = 3;
            log.Write("Attempting an install of the file from '" + sourceFile + "' to '" + destFile + "'.");

            // To be certain that any IOException isn't related to an existing file, first check
            // that the resulting file doesn't already exist.
            if (!overwrite && File.Exists(destFile)) {
                var msg = "Attempted to create '" + destFile + "' from '" + sourceFile
                            + "', but the file already exists. Skipping.";
                throw new IOException(msg);
            }

            // Allow a few retry attempts in case of transient issues.
            for (int i = 0; i < numRetries; i++) {
                try {
                    File.Copy(sourceFile, destFile, overwrite);
                    break;
                }
                catch (Exception) when (i < numRetries - 1) {
                    // As long as there are remaining attempts allowed, wait and try again.
                    // Otherwise, throw the error.
                    Thread.Sleep(500);
                }
            }

            log.Write("Installation of '" + destFile + "' successful.");
        }


        /**
         * In order for neodefaults.cfg to be run when TF2 is launched, autoexec.cfg must execute
         * the file. This method adds the necessary 'exec' statement to the autoexec file in order
         * to accomplish this. If the autoexec.cfg file does not exist, it will be created.
         *
         * neodefaultsPath: The path to the newly-installed neodefaults.cfg file
         *
         * Throws an Exception if an unexpected error occurs.
         */
        private void AppendLinesToAutoExec(String neodefaultsPath) {
            String autoexec;
            String defaultLocation = Path.Combine(tfPath, @"cfg\", "autoexec.cfg");
            String mastercomfigLocation = Path.Combine(tfPath, @"cfg\user", "autoexec.cfg");

            // Check for a Mastercomfig (https://mastercomfig.com/) install. This is a popular
            // plugin used by many players, and it expects autoexec.cfg to be stored in cfg/user/
            // instead of the usual cfg/ directory. In order to fully support these users, the
            // required execution lines must be added to the correct file.
            String[] filePaths = Directory.GetFiles(Path.Combine(tfPath, "custom"),
                                                    "mastercomfig*preset.vpk",
                                                     SearchOption.TopDirectoryOnly);
            bool mastercomfigEnabled = filePaths.Length != 0;
            autoexec = (mastercomfigEnabled) ? mastercomfigLocation : defaultLocation;
            bool alreadyExists = File.Exists(autoexec);


            if (alreadyExists) {
                // Check if the "exec" statement has already been appended to autoexec.cfg.
                using (var fs = File.Open(autoexec, FileMode.Open, FileAccess.Read)) {
                    var reader = new StreamReader(fs);
                    String line;
                    while ((line = reader.ReadLine()) != null) {
                        if (line.Equals(AutoexecHeader)) {
                            // The "exec" statement is already there, so there's no need to
                            // modify anything.
                            return;
                        }
                    }
                }
            }
            else if (mastercomfigEnabled) {
                // If mastercomfig is in use, make sure the "user" directory exists before
                // proceeding. This operation is idempotent.
                Directory.CreateDirectory(Path.Combine(tfPath, @"cfg\user"));
            }

            StringBuilder sb = new StringBuilder();
            if (alreadyExists) {
                sb.Append(Environment.NewLine);
            }
            sb.AppendLine(AutoexecHeader);
            sb.Append("exec ");
            sb.Append(configFolderName);
            sb.Append("/");
            sb.AppendLine(Path.GetFileNameWithoutExtension(neodefaultsPath));
            sb.AppendLine(AutoexecFooter);

            File.AppendAllText(autoexec, sb.ToString());
        }


        /**
         * Installs the custom hitsound. This is executed on a background thread to avoid locking 
         * the UI.
         */
        public async Task<InstallStatus> InstallHitsound() {
            return await Task.Run(() => {
                try {
                    String srcHitPath = Path.Combine(componentsPath, HitVpkName);
                    String destination = Path.Combine(tfPath, @"custom\" + HitVpkName);

                    CopyFile(srcHitPath, destination, true);
                }
                catch (Exception e) {
                    log.WriteErr("An error occurred when trying to install the hitsound:", e.ToString());
                    return InstallStatus.FAIL;
                }

                return InstallStatus.SUCCESS;
            });
        }


        /**
         * Installs custom HUD files to the custom/ directory. This is executed on a background
         * thread to avoid locking the UI.
         */
        public async Task<InstallStatus> InstallHUD() {
            return await Task.Run(() => {
                try {
                    String srcHUDPath = Path.Combine(componentsPath, HudVpkName);
                    String destination = Path.Combine(tfPath, @"custom\" + HudVpkName);

                    CopyFile(srcHUDPath, destination, true);
                }
                catch (Exception e) {
                    log.WriteErr("An error occurred when trying to install the HUD:", e.ToString());
                    return InstallStatus.FAIL;
                }

                return InstallStatus.SUCCESS;
            });
        }


        /**
         * Installs the neodefaults.cfg file. If there is an existing install, it will be
         * overwritten. This is executed on a background thread to avoid locking  the UI.
         */
        public async Task<InstallStatus> InstallConfig() {
            return await Task.Run(() => {
                // First create the tf/cfg/NeoDefaults folder.
                String configFolderPath = "";
                try {
                    configFolderPath = Path.Combine(tfPath, "cfg", configFolderName);
                    Directory.CreateDirectory(configFolderPath);
                }
                catch (Exception e) {
                    log.WriteErr("An error occurred when trying to create the base folder for the config.",
                                    e.ToString());
                    return InstallStatus.FAIL;
                }


                // Create the config file.
                String sourceCfg = "";
                String destCfg = "";
                try {
                    sourceCfg = Path.Combine(componentsPath, sourceCfgName);
                    destCfg = Path.Combine(configFolderPath, destCfgName);

                    // If the config file already exists, remove the read-only attribute to allow
                    // overwriting the file.
                    if (File.Exists(destCfg)) {
                        File.SetAttributes(destCfg, FileAttributes.Normal);
                    }
                    CopyFile(sourceCfg, destCfg, true);

                }
                catch (Exception e) {
                    var logMsg = "An error occurred when trying to create '" + destCfg + "' from '"
                                    + sourceCfg + "'.";
                    log.WriteErr(logMsg, e.ToString());
                    return InstallStatus.FAIL;
                }


                // Create the custom file, if it does not already exist.
                try {
                    String sourceCustom = Path.Combine(componentsPath, customCfgName);
                    String destCustom = Path.Combine(configFolderPath, customCfgName);

                    // If there's already a custom file on the machine, then the user already
                    // has settings defined, so they should not be overridden.
                    if (!File.Exists(destCustom)) {
                        CopyFile(sourceCustom, destCustom, false);
                    }

                }
                catch (Exception e) {
                    log.WriteErr("Failed to create the " + customCfgName + " file.", e.ToString());
                    return InstallStatus.FAIL;
                }


                // Modify autoexec.cfg so that the newly installed config will execute when TF2
                // is launched.
                try {
                    AppendLinesToAutoExec(destCfg);
                }
                catch (Exception e) {
                    log.WriteErr("An error occurred when trying to append to autoexec.cfg.",
                                    e.ToString());

                    // Notify the user that they should modify this manually
                    String message = "Tried to modify 'autoexec.cfg' and failed. To fix this, try"
                                     + " re-running the installation. If the problem persists,"
                                     + " check the FAQ for how steps on how to do this manually.";
                    var dialog = new WarningDialog();
                    dialog.Display(message);
                    return InstallStatus.FAIL;
                }

                log.Write("Config installation complete.");
                return InstallStatus.SUCCESS;
            });
        }
    }
}
