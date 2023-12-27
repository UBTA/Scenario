using EblanDev.ScenarioCore.GameFramework.Data;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{
    /// <summary>
    /// Система работающая в связке со сценарием который управляет ее рантаймом.
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Система попытается вернуть свою дату.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool TryGetData(out IData data);
        
        /// <summary>
        /// Вызывается автоматически.
        /// Система готовится к рантайму.
        /// </summary>
        void Prepare();
        
        /// <summary>
        /// Вызывается автоматически.
        /// Точка входа для юзер кода.
        /// </summary>
        void Init();
        
        /// <summary>
        /// Система выходит из рантайма.
        /// </summary>
        void Exit(bool saveData);
    }
}