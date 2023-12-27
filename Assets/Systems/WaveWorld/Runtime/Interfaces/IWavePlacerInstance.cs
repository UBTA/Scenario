namespace EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces
{
    /// <summary>
    /// Интерфейс для сущности мира ворн.
    /// </summary>
    public interface IWavePlacerInstance
    {
        /// <summary>
        /// Волна на которой сущность активируется.
        /// </summary>
        public int WaveID { get; set; }
        /// <summary>
        /// Тип сущности.
        /// </summary>
        public int GetTypeID { get; }

        /// <summary>
        /// Подготовка сущности к активации.
        /// </summary>
        public void Prepare();
        /// <summary>
        /// Активация сущности.
        /// </summary>
        public void Activate();
    }
}