using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EblanDev.ScenarioCore.Installer.Editor
{
    public sealed class Packages
    {
        public List<PackageContainer> Containers;

        private string PackConfPath => Path.GetFullPath("Packages/com.weackss.scenario.installer/installerConfig.json");

        public void ReadPackages()
        {
            try
            {
                using StreamReader r = new StreamReader(PackConfPath);
                string json = r.ReadToEnd();
                Config conf = JsonUtility.FromJson<Config>(json);
                Containers = conf.Containers;
            }
            catch
            {
                // ignored
            }
        }
    }

    [Serializable]
    sealed class Config
    {
        public List<PackageContainer> Containers;
    }
    
}