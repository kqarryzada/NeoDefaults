namespace NeoDefaults_Installer {
    class ComponentsManager {
        private enum Component {
            HUD,
            hitsound,
            config,
        }

        // Stores which components the user wants installed. It is accessed/indexed using the 'Component'
        // enum. So if a Component's ordinal value is 0, then the 0th element in the array will
        // specify whether the component will be installed or not.
        private readonly bool[] installComponents;


        public ComponentsManager() {
            // Set all installations to true by default, as a "Basic Install" will install
            // everything.
            installComponents = new bool[] { true, true, true };
        }

        /**
         * Enables/disables the installation of the HUD.
         */
        public void SetHUD(bool value) {
            installComponents[(int) Component.HUD] = value;
        }

        /**
         * Enables/disables the installation of the hitsound.
         */
        public void SetHitsound(bool value) {
            installComponents[(int) Component.hitsound] = value;
        }

        /**
         * Enables/disables the installation of the config files.
         */
        public void SetConfig(bool value) {
            installComponents[(int) Component.config] = value;
        }

        /**
         * Returns a boolean indicating whether or not the HUD should be installed.
         */
        public bool HUDInstallEnabled() {
            return installComponents[(int) Component.HUD];
        }

        /**
         * Returns a boolean indicating whether or not the hitsound should be installed.
         */
        public bool HitsoundInstallEnabled() {
            return installComponents[(int) Component.hitsound];
        }

        /**
         * Returns a boolean indicating whether or not the config should be installed.
         */
        public bool ConfigInstallEnabled() {
            return installComponents[(int) Component.config];
        }

        /**
         * Returns the current number of components that will be installed.
         */
        public int NumberOfEnabledComponents() {
            int count = 0;
            foreach (bool componentEnabled in installComponents) {
                if (componentEnabled)
                    count++;
            }

            return count;
        }
    }
}
