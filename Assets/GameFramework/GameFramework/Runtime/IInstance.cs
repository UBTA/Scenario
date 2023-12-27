namespace EblanDev.ScenarioCore.GameFramework
{
    /// <summary>
    /// Базовая сущность c которой работают Workers.
    /// </summary>
    public interface IInstance
    {
        /// <summary>
        /// Задействована ли сущность в рантайме.
        /// </summary>
        public bool IsBusy { get; }

        /// <summary>
        /// Вызывается автоматически при создании через Workers.
        /// Точка входа для юзер кода.
        /// Сущность вошла в рантайм.
        /// </summary>
        public void Init();

        /// <summary>
        /// Вызывается автоматически при очистке Worker.Clear().
        /// Сущность выходит из рантайма
        /// </summary>
        public void End();
    }
}