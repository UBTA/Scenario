using System.Collections.Generic;
using System.Threading.Tasks;
using EblanDev.ScenarioCore.GameFramework.Systems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework
{
    public abstract class Scenario : MonoBehaviour
    {
        protected List<ISystem> _systems;

        private void Start()
        {
            _systems = new List<ISystem>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.TryGetComponent<ISystem>(out var result))
                {
                    _systems.Add(result);
                }
            }
            
            Prepare();
            GameScenario();
        }

        protected virtual void Prepare()
        {
            foreach (var system in _systems)
            {
                system.Prepare();
            }

            foreach (var system in _systems)
            {
                system.Init();
            }
        }
        
        protected void Exit(bool saveData = true)
        {
            foreach (var syst in _systems)
            {
                syst.Exit(saveData);
            }
        }
        
        protected abstract Task GameScenario();
    }
}