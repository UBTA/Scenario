using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Data
{
    /// <summary>
    /// Чистая скриптабл дата без работы с префс.
    /// </summary>
    public class DataClear : ScriptableObject, IData
    {
        /// <summary>
        /// Подготовка даты к рантайму.
        /// </summary>
        public virtual void Read()
        {
        }

        /// <summary>
        /// Выход даты из рантайма.
        /// </summary>
        public virtual void Write()
        {
        }
    }
}