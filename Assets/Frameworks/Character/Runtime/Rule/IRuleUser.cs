namespace EblanDev.ScenarioCore.CharacterFramework.Rules
{
    public interface IRuleUser
    {
        public IRuleUser AddRule(IRule rule);
        public IRuleUser SetRuleBook(RuleBook rules);
        public IRuleUser RemoveRule<R>() where R : IRule;
    }
}