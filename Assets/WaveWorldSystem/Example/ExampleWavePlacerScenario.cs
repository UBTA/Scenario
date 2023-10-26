using System;
using System.Threading.Tasks;
using FAwesome.ScenarioCore.GameFramework;
using UnityEngine;

namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    public class ExampleWavePlacerScenario : Scenario
    {
        [SerializeField] private ExampleWavePlacerSystem WavePlacer;
        
        protected override async Task GameScenario()
        {
            WavePlacer.CreateWorld(null);
            
            while (true)
            {
                WavePlacer.PrepareWave();

                if (WavePlacer.IsNoMoreWaves())
                {
                    return;
                }
                
                await Task.Delay(TimeSpan.FromSeconds(3f));

                WavePlacer.ActivateWave();

                await Task.Delay(TimeSpan.FromSeconds(5f));
            }
        }
    }
}