using System;
using EblanDev.ScenarioCore.GameFramework;
using EblanDev.ScenarioCore.UIFramework.Bus;
using UnityEngine;

namespace EblanDev.ScenarioCore.UIFramework
{
    /// <summary>
    /// Внедрение в работу сценария интерфейса UI.
    /// </summary>
    /// <typeparam name="B">
    /// Тип шины (необходимо унаследовааться от UIBus).
    /// </typeparam>
    public abstract class UIScenario<B> : Scenario where B : UIBus
    {
        /// <summary>
        /// Ссылка на менеджер интерфейса.
        /// </summary>
        [SerializeField] protected UI<B> UI;
        
        /// <summary>
        /// Ссылка на шину интерфейса.
        /// </summary>
        protected B Bus;

        /// <summary>
        /// Подготовка сценария и систем к работе.
        /// </summary>
        protected override void Prepare()
        {
            Bus = (B) Activator.CreateInstance(typeof(B));
            
            foreach (var system in _systems)
            {
                system.Prepare();
                if (system.TryGetData(out var SOData))
                {
                    Bus.AddData(SOData);
                }
            }

            foreach (var system in _systems)
            {
                system.Init();
            }

            if (Bus != null)
            {
                Bus.Init();
            }
            else
            {
                Debug.LogWarning("Bus not set!");
            }
            
            UI.Init(Bus);
        }
    }
}