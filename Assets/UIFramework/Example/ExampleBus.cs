using FAwesome.ScenarioCore.UIFramework.Bus;

namespace FAwesome.ScenarioCore.UIFramework.Example
{
    public class ExampleBus : UIBus
    {
        public readonly BusEvent<int> GameSession = new BusEvent<int>();
        public readonly BusEvent<float> GameTimer = new BusEvent<float>();
    }
}