using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;

namespace EblanDev.ScenarioCore.UIFramework.Bus
{
    /// <summary>
    /// Ивентовая шина для отслеживаания игровых событий
    /// </summary>
    public class UIBus
    {
        private List<IData> Datas = new List<IData>();

        /// <summary>
        /// Добавляет дату в шину, если такого типа даты еще нет.
        /// </summary>
        /// <param name="data">
        /// Ссылка на дату
        /// </param>
        public void AddData(IData data)
        {
            foreach (var SOData in Datas)
            {
                if (data.GetType() == SOData.GetType())
                {
                    return;
                }
            }
            
            Datas.Add(data);
        }

        /// <summary>
        /// Получение даты типа TData
        /// </summary>
        /// <typeparam name="TData">
        /// Дженерик дата IData
        /// </typeparam>
        /// <returns>
        /// Возвращает объект даты либо нул
        /// </returns>
        public TData Data<TData>() where TData : IData
        {
            foreach (var SOData in Datas)
            {
                if (SOData is TData data)
                {
                    return data;
                }
            }

            return default;
        }
        
        /// <summary>
        /// Инициализация шины
        /// </summary>
        public virtual void Init()
        {
            
        }
    }
}