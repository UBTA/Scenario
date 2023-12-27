namespace EblanDev.ScenarioCore.CharacterFramework.Rules
{
    public interface IRule
    {
        public void Start(Puppet puppet, RuleBook rules);
        public void Check();
        public void Stop();
    }
}