using System.Collections.Generic;
using System.Threading.Tasks;
using EblanDev.ScenarioCore.GameFramework.Systems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EblanDev.ScenarioCore.GameFramework
{
    public abstract class Scenario : SerializedMonoBehaviour
    {
        [OdinSerialize] protected List<ISystem> systems;

        private void Start()
        {
            Prepare();
            GameScenario();
        }

        protected virtual void Prepare()
        {
            foreach (var system in systems)
            {
                system.Prepare();
            }

            foreach (var system in systems)
            {
                system.Init();
            }
        }
        
        protected void Exit(bool saveData = true)
        {
            foreach (var syst in systems)
            {
                syst.Exit(saveData);
            }
        }
        
        protected abstract Task GameScenario();
    }
}