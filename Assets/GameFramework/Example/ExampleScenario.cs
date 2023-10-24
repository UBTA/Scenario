using System;
using System.Threading.Tasks;
using UnityEngine;

namespace FAwesome.ScenarioCore.GameFramework.Example
{
    public class ExampleScenario : Scenario<ExampleBus>
    {
        [SerializeField] private ExampleSystem exampleSystem;
        
        protected override async Task GameScenario()
        {
            var session = exampleSystem.GameSession;
            
            Debug.Log("This is session number " + session);
            
            Bus.GameSession?.Invoke(session);

            var lastInvokedSession = Bus.GameSession?.LastInvokeData;

            var exData = Bus.Data<ExampleData>(); //получение даты используется редко, но возможность имеется
            
            Debug.Log("This is last invoked session number " + lastInvokedSession);
            
            await Task.Delay(TimeSpan.FromSeconds(10));
            
            ExitSystems();
        }
    }
}