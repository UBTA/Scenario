using UnityEngine.UIElements;

namespace EblanDev.ScenarioCore.Installer.Editor
{
    sealed class WindowStyle
    {
        private InstallerColorPalette Colors = new InstallerColorPalette();
        
        public Box Box()
        {
            var box = new Box();
            box.style.height = 2;
            box.style.backgroundColor = new StyleColor(Colors.Color4);
            
            return box;
        }
        
        public Label Label(string text, int fontSize)
        {
            var label = new Label();
            label.text = text;
            label.style.fontSize = fontSize;
            label.style.height = fontSize * 2;
            label.style.color = new StyleColor(Colors.Color3);
            
            return label;
        }
        
        public Button Button(string text)
        {
            var btn = new Button();
            btn.style.height = 60;
            btn.style.width = 200;
            btn.style.backgroundColor = new StyleColor(Colors.Color4);
            btn.text = text;
            btn.style.color = new StyleColor(Colors.Color3);
            btn.style.fontSize = 40;

            return btn;
        }
    }
}