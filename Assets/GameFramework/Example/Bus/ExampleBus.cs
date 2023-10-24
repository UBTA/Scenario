using FAwesome.ScenarioCore.GameFramework.Bus;

namespace FAwesome.ScenarioCore.GameFramework.Example
{
    public class ExampleBus : UIBus
    {
        public readonly BusEvent<int> GameSession = new BusEvent<int>();
        public readonly BusEvent<float> GameTimer = new BusEvent<float>();
    }
}