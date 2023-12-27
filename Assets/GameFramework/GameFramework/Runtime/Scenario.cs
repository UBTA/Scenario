using System.Collections.Generic;
using System.Threading.Tasks;
using EblanDev.ScenarioCore.GameFramework.Systems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EblanDev.ScenarioCore.GameFramework
{
    /// <summary>
    /// Инициализация систем в Start().
    /// Вызов сценария в Start().
    /// </summary>
    public abstract class Scenario : SerializedMonoBehaviour
    {
        /// <summary>
        /// Список систем
        /// </summary>
        [OdinSerialize] protected List<ISystem> systems;

        private void Start()
        {
            Prepare();
            GameScenario();
        }

        /// <summary>
        /// Подготовка сценария и систем к работе.
        /// </summary>
        protected virtual void Prepare()
        {
            foreach (var system in systems)
            {
                system.Prepare();
            }

            foreach (var system in systems)
            {
                system.Init();
            }
        }
        

        /// <summary>
        /// Завершает работу всех систем сохраняя или не сохраняя данные
        /// </summary>
        /// <param name="saveData"></param>
        protected void ExitSystems(bool saveData = true)
        {
            foreach (var syst in systems)
            {
                syst.Exit(saveData);
            }
        }

        /// <summary>
        /// Метод для объявления игрового сценария.
        /// </summary>
        /// <returns></returns>
        protected abstract Task GameScenario();
    }
}