using FAwesome.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    public class ExampleWavePlacerInstanceWorker : WorkerPulled<ExampleWavePlacerInstance>
    {
        public ExampleWavePlacerInstanceWorker(Transform _parent) : base(_parent)
        {
        }
        
        protected override bool Compare(ExampleWavePlacerInstance instance, ExampleWavePlacerInstance anotherInstance)
        {
            return instance.GetTypeID == anotherInstance.GetTypeID;
        }
    }
}