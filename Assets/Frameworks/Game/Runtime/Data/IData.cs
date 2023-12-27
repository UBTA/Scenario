namespace EblanDev.ScenarioCore.GameFramework.Data
{
    /// <summary>
    /// Дата для конфигурации систем.
    /// Cистема встраивает дату в свой цикл жизни автоматически.
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// Подготовка даты к рантайму.
        /// </summary>
        void Read();
        
        /// <summary>
        /// Выход даты из рантайма.
        /// </summary>
        void Write();
    }
}