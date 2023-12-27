using EblanDev.ScenarioCore.UIFramework.Bus;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.UIFramework.UIElements
{
    /// <summary>
    /// Сущность для описания поведения определенного экрана
    /// </summary>
    /// <typeparam name="B">
    /// Дженерик типа шины с которой будет работать экран
    /// </typeparam>
    public class ScreenView<B> : View where B : UIBus
    {
        /// <summary>
        /// Если true то будет игнорировать все вызовы Show и Hide цепочки переключения экранов в UI 
        /// </summary>
        [FoldoutGroup("Settings", order: -1)] [SerializeField] public bool Ignore = false;
        /// <summary>
        /// Если true то автоматически будет показан после Init
        /// </summary>
        [FoldoutGroup("Settings", order: -1)] [SerializeField] public bool ShowOnInit = false;

        /// <summary>
        /// Ссылка на шину
        /// </summary>
        protected B Bus;
        
        /// <summary>
        /// Сетает шину для работы экрана
        /// </summary>
        /// <param name="bus">
        /// Ссылка на шину
        /// </param>
        public void SetBus(B bus)
        {
            Bus = bus;
        }
    }
}