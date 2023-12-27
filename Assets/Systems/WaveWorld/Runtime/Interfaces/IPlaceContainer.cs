using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces
{
    /// <summary>
    /// Контейнер для информации о точке в мире волн.
    /// </summary>
    public interface IPlaceContainer
    {
        /// <summary>
        /// Тип сущности.
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// Волна на которой сущность активируется.
        /// </summary>
        public int Wave { get; set; }

        public Vector3 SpawnPoint { get; set; }
        public Vector3 SpawnRotation { get; set; }
        public Vector3 SpawnScale { get; set; }
    }
}