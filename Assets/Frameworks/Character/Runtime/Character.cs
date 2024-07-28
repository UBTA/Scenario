using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Behaviour;
using EblanDev.ScenarioCore.CharacterFramework.Rules;
using EblanDev.ScenarioCore.GameFramework;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework
{
    public class Character : Instance, IBehaviourUser, IRuleUser
    {
        [SerializeField] protected float RulesCheckStep = 0.1f;
        
        protected Puppet Puppet;
        protected RuleBook Rules;

        protected bool isInited;
        
        public override void Init()
        {
            Puppet = new Puppet(transform);
            Rules = new RuleBook(RulesCheckStep, Puppet);
            base.Init();
            isInited = true;
        }

        protected virtual void FixedUpdate()
        {
            if (isInited == false)
            {
                return;
            }
            Puppet.Fixed();
        }

        protected virtual void LateUpdate()
        {
            if (isInited == false)
            {
                return;
            }
            Puppet.Late();
        }

        public async UniTask Behave(BehaviourTree tree, CancellationToken token = default)
        {
            foreach (var command in tree.Commands)
            {
                await command.Execute(Puppet, token);
            }
        }
        public async UniTask Behave(ICommand command, CancellationToken token = default)
        {
            await command.Execute(Puppet, token);
        }
        
        public IRuleUser AddRule(IRule rule)
        {
            Rules.Add(rule);
            return this;
        }
        
        public IRuleUser RemoveRule<R>() where R : IRule
        {
            Rules.Remove<R>();   
            return this;
        }

        public override void End()
        {
            isInited = false;
            base.End();
            Rules.Stop();
        }
    }
}