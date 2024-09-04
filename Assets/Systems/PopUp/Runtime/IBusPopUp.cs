using EblanDev.ScenarioCore.UIFramework.Bus;

namespace EblanDev.ScenarioCore.Systems.PopUpSystemUnit
{
    public interface IBusPopUp<Bus> where Bus: UIBus
    {
        public void SetupBus(Bus bus);
    }
}