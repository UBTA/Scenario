using FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Data;
using UnityEngine;

namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    [CreateAssetMenu(fileName = "ExampleWavePlacerData", menuName = "ScenarioFramework/Example/ExampleWavePlacerData", order = 1)]
    public class ExampleWavePlacerData : WavePlacerData<ExampleWavePlacerWorld, ExamplePlaceContainer, ExampleWavePlacerInstance>
    {
        
    }
}