using System.Threading.Tasks;
using DG.Tweening;
using FAwesome.ScenarioCore.GameFramework.Example;
using UnityEngine;

namespace FAwesome.ScenarioCore.UIFramework.Example
{
    public class ExampleUIScenario : UIScenario<ExampleBus>
    {
        [SerializeField] private ExampleSystem exampleSystem;
        
        protected override async Task GameScenario()
        {
            var session = exampleSystem.GameSession;
            Bus.GameSession?.Invoke(session);
            
            Debug.Log("This is session number " + session);
            
            UI.Show<ExampleScreen>();

            await DOVirtual.Float(0f, 10f, 10f, (v) =>
            {
                Bus.GameTimer?.Invoke(v);
                
            }).AsyncWaitForCompletion();
            
            ExitSystems();
        }
    }
}