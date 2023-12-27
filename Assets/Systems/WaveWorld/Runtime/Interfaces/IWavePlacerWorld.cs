using System.Collections.Generic;

namespace EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces
{
    /// <summary>
    /// Интерфейс для реализации мира волн.
    /// </summary>
    /// <typeparam name="P"></typeparam>
    public interface IWavePlacerWorld<P> where P : IPlaceContainer
    {
        /// <summary>
        /// Набор контейнеров точек мира.
        /// </summary>
        public List<P> Points { get; set; }
    }
}