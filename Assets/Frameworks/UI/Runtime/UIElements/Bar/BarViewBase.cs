using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;


namespace EblanDev.ScenarioCore.UIFramework.UIElements
{
    /// <summary>
    /// Базовая абстракция бара.
    /// </summary>
    public abstract class BarViewBase : View
    {
        /// <summary>
        /// Текст в которй быр сетает свои значения.
        /// </summary>
        [OdinSerialize] [FoldoutGroup("Text Settings")] protected TextMeshProUGUI[] Text;

        /// <summary>
        /// Показатель заполнения 0.0f-1.0f.
        /// </summary>
        protected float fillAmount;

        /// <summary>
        /// Публичный геттер показателя заполнения 0.0f-1.0f.
        /// </summary>
        public float FillAmount => fillAmount;
        
        /// <summary>
        /// Сетает значение бара.
        /// </summary>
        /// <param name="amount">Значение заполнения 0.0f-1.0f.</param>
        /// <param name="text">Текст вписываемый в бар.</param>
        /// <param name="textPrefix">Префикс для текста.</param>
        /// <param name="textPostfix">Постфикс для текста.</param>
        public void SetFillAmount(float amount, string text = null, string textPrefix = null, string textPostfix = null)
        {
            fillAmount = Mathf.Clamp01(amount);

            for (var i = 0; i < Text.Length; i++)
            {
                if (Text[i] != null)
                {
                    Text[i].text = textPrefix + text + textPostfix;
                }
            }

            Redraw();
        }
        
        /// <summary>
        /// Вызов перерисовки филлера бара
        /// </summary>
        protected abstract void Redraw();
    }
}