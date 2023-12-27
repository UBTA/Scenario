using EblanDev.ScenarioCore.GameFramework;
using EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Data;
using EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit
{
    
    /// <summary>
    /// Эдитор монобех для удобного фила миров волн.
    /// </summary>
    /// <typeparam name="WI">
    /// Инстанс мира волн
    /// </typeparam>
    /// <typeparam name="I">
    /// Инстанс размещаемой сущности
    /// </typeparam>
    /// <typeparam name="D">
    /// Скриптабл дата системы.
    /// </typeparam>
    /// <typeparam name="P">
    /// Контейнер точки в мире
    /// </typeparam>
    public interface IWavePlacerWorldFiller<WI, I, D, P>
        where WI : MonoBehaviour, IInstance, IWavePlacerWorld<P>
        where I : MonoBehaviour, IInstance, IWavePlacerInstance
        where D : WavePlacerData<WI, P, I>
        where P : IPlaceContainer
    {
        /// <summary>
        /// Создание мира в инспеторе.
        /// </summary>
        void Create();
        /// <summary>
        /// Добавление в мир новой сущности.
        /// </summary>
        /// <param name="point"></param>
        void Add(P point);
        /// <summary>
        /// Сохранение эдитор мира в мир волн
        /// </summary>
        void End();
    }
}