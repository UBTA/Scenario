using EblanDev.ScenarioCore.GameFramework.Data;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{ 
    /// <summary>
    /// Реализация статик системы с датой.
    /// Предоставляет возможность вызовов в статику. 
    /// </summary>
    /// <typeparam name="D">
    /// Тип скриптабл даты.
    /// </typeparam>
    public class StaticSystemDated<D> : SystemDated<D> where D : IData
    {
        /// <summary>
        /// Статическое хранилище даты.
        /// Доступно к моменту Init().
        /// </summary>
        protected static D DataStatic;

        /// <summary>
        /// Вызывается автоматически.
        /// Система готовится к рантайму.
        /// Вызывает Read() у своей даты.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
            DataStatic = Data;
        }

        /// <summary>
        /// Вызывается автоматически.
        /// Точка входа для юзер кода.
        /// </summary>
        public override void Init()
        {
        }
    }
}