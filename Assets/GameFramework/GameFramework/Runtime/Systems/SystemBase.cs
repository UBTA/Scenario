using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{
    
    /// <summary>
    /// Базовая реализация системы не использующей дату.
    /// </summary>
    public abstract class SystemBase : MonoBehaviour, ISystem
    {
        /// <summary>
        /// Система попытается вернуть свою дату.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool TryGetData(out IData data)
        {
            data = null;
            return false;
        }

        /// <summary>
        /// Вызывается автоматически.
        /// Система готовится к рантайму.
        /// </summary>
        public abstract void Prepare();

        /// <summary>
        /// Вызывается автоматически.
        /// Точка входа для юзер кода.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Система выходит из рантайма.
        /// </summary>
        public abstract void Exit(bool saveData);
    }
}