using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

namespace EblanDev.ScenarioCore.Installer.Editor
{
    public sealed class WindowActions
    {
        public async Task Install(PackageContainer container, string ver)
        {
            if (ver == null)
            {
                ver = container.AvailableVersions.Last();
            }
            
            Debug.Log(container.Link + container.Tag + ver);
            
            var request = Client.Add(container.Link + container.Tag + ver);

            while (!request.IsCompleted)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));
            }
            
            if (request.Error != null)
            {
                Debug.LogError(request.Error.message);
            }
        }
        
        public async Task UnInstall(PackageContainer container)
        {
            Debug.Log(container.Name);
            var request = Client.Remove(container.Name);

            while (!request.IsCompleted)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));
            }
            
            if (request.Error != null)
            {
                Debug.LogError(request.Error.message);
            }
        }

        public async Task InitInstaller(Packages packs)
        {
            var packages = Client.List();
            while (!packages.IsCompleted)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));
            }

            foreach (var container in packs.Containers)
            {
                foreach (var result in packages.Result)
                {
                    if (container.Name.Equals(result.name))
                    {
                        container.isInstalled = true;
                        
                        container.Description = result.description;
                        container.Version = result.version;
                        
                        break;
                    }

                    container.isInstalled = false;
                }
            }
        }
    }
}