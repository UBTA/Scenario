using System;

namespace EblanDev.ScenarioCore.Installer.Editor
{
    [Serializable]
    public sealed class PackageContainer
    { 
        public string DisplayName;
        public string Name;
        public string Version;
        public string Description;
        public string Link;

        public string Tag;
        
        public string[] AvailableVersions;
        
        public bool isInstalled;
    }
}