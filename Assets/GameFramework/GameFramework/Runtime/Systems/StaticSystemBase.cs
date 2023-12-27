namespace EblanDev.ScenarioCore.GameFramework.Systems
{
    /// <summary>
    /// Реализация базовой статик системы.
    /// Предоставляет возможность вызовов в статику. 
    /// </summary>
    public class StaticSystemBase : SystemBase
    {
        /// <summary>
        /// Вызывается автоматически.
        /// Система готовится к рантайму.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Вызывается автоматически.
        /// Точка входа для юзер кода.
        /// </summary>
        public override void Init()
        {
        }

        /// <summary>
        /// Система выходит из рантайма.
        /// </summary>
        public override void Exit(bool saveData = true)
        {
        }
    }
}