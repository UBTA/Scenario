using EblanDev.ScenarioCore.GameFramework.Data;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{
    public interface ISystem
    {
        bool TryGetData(out IData data);
        
        void Prepare();
        
        void Init();
        
        void Exit(bool saveData);
    }
}