using FAwesome.ScenarioCore.GameFramework.Workers;

namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    public class ExampleWavePlacerSystem : WavePlacerSystem<
        ExampleWavePlacerWorld, 
        ExampleWavePlacerInstance, 
        ExampleWavePlacerData, 
        ExamplePlaceContainer>
    {
        public override void Init()
        {
            world = new WorkerSolo<ExampleWavePlacerWorld>(transform);
            instances = new ExampleWavePlacerInstanceWorker(transform);
        }

        public bool IsNoMoreWaves()
        {
            return preparedWave.Count == 0;
        }
    }
}