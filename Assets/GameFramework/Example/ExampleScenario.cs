using System;
using System.Threading.Tasks;
using UnityEngine;

namespace FAwesome.ScenarioCore.GameFramework.Example
{
    public class ExampleScenario : Scenario
    {
        [SerializeField] private ExampleSystem exampleSystem;
        
        protected override async Task GameScenario()
        {
            var session = exampleSystem.GameSession;
            Debug.Log("This is session number " + session);

            await Task.Delay(TimeSpan.FromSeconds(10));
            
            ExitSystems();
        }
    }
}