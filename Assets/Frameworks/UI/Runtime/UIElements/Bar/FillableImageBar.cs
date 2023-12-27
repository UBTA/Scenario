using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace EblanDev.ScenarioCore.UIFramework.UIElements
{
    /// <summary>
    /// Базовая реализация бара на филлабл Image компоненте.
    /// </summary>
    public class FillableImageBar : BarViewBase
    {
        /// <summary>
        /// Ссылка на Image компонент.
        /// </summary>
        [FoldoutGroup("Fill settings")] [SerializeField] protected Image fillImage;
        
        /// <summary>
        /// Вызов перерисовки филлера бара
        /// </summary>
        protected override void Redraw()
        {
            if(fillImage) fillImage.fillAmount = fillAmount;
        }
    }
}