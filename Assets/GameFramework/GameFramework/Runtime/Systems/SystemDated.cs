using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{
    /// <summary>
    /// Система работающяя с датой.
    /// </summary>
    /// <typeparam name="D">
    /// Тип даты.
    /// </typeparam>
    public abstract class SystemDated<D> : SystemBase where D : IData
    {
        /// <summary>
        /// Xранилище даты.
        /// Доступно к моменту Init().
        /// </summary>
        [SerializeField] protected D Data;

        /// <summary>
        /// Система попытается вернуть свою дату.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool TryGetData(out IData data)
        {
            data = null;
            
            if (Data == null)
            {
                return false;
            }

            data = Data;
            return true;
        }

        /// <summary>
        /// Вызывается автоматически.
        /// Система готовится к рантайму.
        /// Вызывает Read() у своей даты.
        /// </summary>
        public override void Prepare()
        {
            if (Data != null)
            {
                Data.Read();
            }
        }

        /// <summary>
        /// Система выходит из рантайма.
        /// Если saveData==true то вызовет у своей даты Write().
        /// </summary>
        public override void Exit(bool saveData = true)
        {
            if (saveData)
            {
                if (Data != null)
                {
                    Data.Write();
                }
            }
        }
    }
}