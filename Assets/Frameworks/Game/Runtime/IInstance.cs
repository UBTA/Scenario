namespace EblanDev.ScenarioCore.GameFramework
{
    public interface IInstance
    {
        public bool IsBusy { get; }

        public void Init();

        public void End();
    }
}