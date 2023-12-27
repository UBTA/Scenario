using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;
using EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Data
{
    /// <summary>
    /// Скриптабл дата для системы волн
    /// </summary>
    /// <typeparam name="WI">
    /// Инстанс мира волн
    /// </typeparam>
    /// <typeparam name="P">
    /// Контейнер точки в мире
    /// </typeparam>
    /// <typeparam name="I">
    /// Инстанс размещаемой сущности
    /// </typeparam>
    public class WavePlacerData<WI, P, I> : DataClear
        where P : IPlaceContainer 
        where WI : IWavePlacerWorld<P>
        where I : IWavePlacerInstance
    {
        [SerializeField] private List<I> instances;
        [SerializeField] private WI worldInstance;

        /// <summary>
        /// Получение размещаемой сущности
        /// </summary>
        /// <param name="typeID">
        /// Айди сущности
        /// </param>
        /// <returns>
        /// Возвращает вариант сущности
        /// </returns>
        public I GetInstance(int typeID) 
        {
            foreach (var inst in instances)
            {
                if (inst.GetTypeID == typeID)
                {
                    return inst;
                } 
            }

            return instances[0];
        }

        /// <summary>
        /// Поулчение сущности мира волн
        /// </summary>
        public virtual WI GetWorld()
        {
            return worldInstance;   
        }
    }
}