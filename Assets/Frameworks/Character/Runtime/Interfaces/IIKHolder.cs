using EblanDev.ScenarioCore.CharacterFramework.Modules.IKHolder;

namespace EblanDev.ScenarioCore.CharacterFramework.Interfaces
{
    public interface IIKHolder : ICharacterModule, IFixed
    {
        public Grab Left { get; }
        public Grab Right { get; }
        public Grab TwoHand { get; }

        public void Hold(IHolderItem item, bool left = true, bool right = true);
        public void Drop(bool left = true, bool right = true);
    }
}