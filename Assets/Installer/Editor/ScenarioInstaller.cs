using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EblanDev.ScenarioCore.Installer.Editor
{
    sealed class ScenarioInstaller : EditorWindow
    {
        private ListView leftPane;
        private VisualElement rightPane;

        private InstallerColorPalette Colors = new InstallerColorPalette();
        private WindowStyle Style = new WindowStyle();
        
        public WindowActions Actions = new WindowActions();
        public Packages Packs = new Packages();

        private string selectedVersion;

        [MenuItem("Scenario/Installer")]
        public static void ShowMyEditor()
        {
            EditorWindow wnd = GetWindow<ScenarioInstaller>();
            wnd.titleContent = new GUIContent("Scenario Installer");
        } 
      
        public async void CreateGUI()
        {
            Packs.ReadPackages();
            
            if (Packs.Containers == null)
            {
                return;
            }
            
            rootVisualElement.Clear();

            await Actions.InitInstaller(Packs);
            
            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
      
            rootVisualElement.Add(splitView);

            leftPane = new ListView();
            splitView.Add(leftPane);
            leftPane.style.backgroundColor = new StyleColor(Colors.Color2);
            
            rightPane = new VisualElement();
            splitView.Add(rightPane);
            rightPane.style.backgroundColor = new StyleColor(Colors.Color2);
            
            leftPane.itemsSource = Packs.Containers;
            leftPane.makeItem = () => new Label();
            leftPane.bindItem = (item, index) =>
            {
                if (item is Label labelItem)
                {
                  labelItem.style.fontSize = 15;
                  labelItem.style.unityFontStyleAndWeight = FontStyle.Bold;
        
                    if (Packs.Containers[index].isInstalled)
                    {
                        labelItem.text = Packs.Containers[index].DisplayName + "(Installed)";
                        labelItem.style.color = new StyleColor(Colors.True);
                    }
                    else
                    {
                        labelItem.text = Packs.Containers[index].DisplayName + "(Not Installed)";
                        labelItem.style.color = new StyleColor(Colors.False);
                    }
                }
            };

            leftPane.onSelectionChange += OnSpriteSelectionChange;
        }

        private void OnSpriteSelectionChange(IEnumerable<object> enumerable)
        {
            rightPane.Clear();
            selectedVersion = null;

            var objects = enumerable as object[] ?? enumerable.ToArray();
            if (objects.First() == null)
            {
                return;
            }

            var package = objects.First() as PackageContainer;

            var infoButtonsSplit = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Vertical);

            rightPane.Add(infoButtonsSplit);

            var rightUpPane = new VisualElement();
            infoButtonsSplit.Add(rightUpPane);
            rightUpPane.style.backgroundColor = new StyleColor(Colors.Color2);

            var rightDownPane = new VisualElement();
            infoButtonsSplit.Add(rightDownPane);
            rightDownPane.style.backgroundColor = new StyleColor(Colors.Color2);

            rightUpPane.Add(Style.Label(package.DisplayName, 30));
            rightUpPane.Add(Style.Box());
            rightUpPane.Add(Style.Label("Version: " + package.Version, 16));
            rightUpPane.Add(Style.Box());
            rightUpPane.Add(Style.Label("Description: " + package.Description, 16));

            var buttonsSplit = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

            rightDownPane.Add(buttonsSplit);

            var buttonsSplitLeftPane = new VisualElement();
            buttonsSplit.Add(buttonsSplitLeftPane);
            buttonsSplitLeftPane.style.backgroundColor = new StyleColor(Colors.Color2);

            var versionsList = new ListView();
            buttonsSplit.Add(versionsList);
            versionsList.style.backgroundColor = new StyleColor(Colors.Color2);

            versionsList.itemsSource = package.AvailableVersions;
            versionsList.makeItem = () => new Label();
            versionsList.bindItem = (item, index) =>
            {
                if (item is Label labelItem)
                {
                    labelItem.style.fontSize = 15;
                    labelItem.style.unityFontStyleAndWeight = FontStyle.Bold;
                    labelItem.text = package.AvailableVersions[index].ToString();
                    
                    if (package.isInstalled)
                    {
                        if (package.AvailableVersions[index].Contains(package.Version))
                        {
                            labelItem.style.color = new StyleColor(Colors.True);
                        }
                        else
                        {
                            labelItem.style.color = new StyleColor(Colors.Color3);
                        }
                    }
                    else
                    {
                        labelItem.style.color = new StyleColor(Colors.Color3);
                    }
                }
            };

            versionsList.onSelectionChange += OnVersionSelect;

            var btn2 = Style.Button("Install");
            buttonsSplitLeftPane.Add(btn2);
            btn2.clickable.clicked += async () =>
            {
                await Actions.Install(package, selectedVersion);
                CreateGUI();
            };
            
            rightUpPane.Add(Style.Box());
            
            var btn = Style.Button("Uninstall");
            buttonsSplitLeftPane.Add(btn);
            btn.clickable.clicked += async () =>
            {
                await Actions.UnInstall(package);
                CreateGUI();
            };
        }

        private void OnVersionSelect(IEnumerable<object> enumerable)
        {
            var objects = enumerable as object[] ?? enumerable.ToArray();
            if (objects.First() == null)
            {
                return;
            }
            
            var ver = objects.First() as string;
            selectedVersion = ver;
        }
    }
}