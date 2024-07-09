using EblanDev.ScenarioCore.UIFramework.UIElements;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.PopUpSystemUnit
{
    public class PopUpView : View
    {
        [SerializeField] private ClickableView exitBtn;

        public override void Init()
        {
            base.Init();
            exitBtn.Init();
            exitBtn.OnClickEvent += CloseClick;
        }

        protected void CloseClick()
        {
            Hide();
        }
    }
}