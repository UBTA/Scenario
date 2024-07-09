using TMPro;
using UnityEngine;

namespace EblanDev.ScenarioCore.UIFramework.UIElements
{
    /// <summary>
    /// Базовый view счетчик.
    /// Умеет выводить на экран значения и анимироваться при их изменении.
    /// </summary>
    public class CounterView : View
    {
        [SerializeField] private string prefix;
        [SerializeField] private string postfix;
        [SerializeField] private TextMeshProUGUI text;

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            base.Init();
            SetValueImmediately("0");
        }

        /// <summary>
        /// Устанавливает новое значение используя анимацию Attention.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            text.text = prefix + value + postfix;
        }

        /// <summary>
        /// Устанавливает новый префикс для значения счетчика.
        /// </summary>
        /// <param name="prefix">
        /// </param>
        public void SetPrefix(string prefix)
        {
            this.prefix = prefix;
        }

        /// <summary>
        /// Устанавливает новый постфикс для значения счетчика.
        /// </summary>
        /// <param name="postfix">
        /// </param>
        public void SetPostfix(string postfix)
        {
            this.postfix = postfix;
        }
        
        /// <summary>
        /// Устанавливает новое значение без анимации.
        /// </summary>
        /// <param name="value"></param>
        public void SetValueImmediately(string value)
        {
            text.text = prefix + value + postfix;
        }
    }
}