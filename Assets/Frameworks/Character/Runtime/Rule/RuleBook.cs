using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace EblanDev.ScenarioCore.CharacterFramework.Rules
{
    public class RuleBook
    {
        private List<IRule> Rules;
        private Puppet RulesTarget;

        private float checkStep;
        
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;

        private List<IRule> addQueue;
        private List<Type> removeQueue;

        public RuleBook(float step, Puppet puppet)
        {
            Rules = new List<IRule>();

            addQueue = new List<IRule>();
            removeQueue = new List<Type>();
            
            RulesTarget = puppet;
            checkStep = step;
            
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            
            Start();
        }

        public bool TryGet<R>(out R res) where R : IRule
        {
            foreach (var rule in Rules)
            {
                if (rule is R)
                {
                    res = (R)rule;
                    return true;
                }
            }

            res = default;
            return false;
        }
        
        public void Add(IRule rule)
        {
            rule.Start(RulesTarget, this);
            addQueue.Add(rule);
        }

        public void Remove<R>() where R : IRule
        {
            if (TryGet<R>(out var rule))
            {
                rule.Stop();
            }
            removeQueue.Add(typeof(R));
        }

        public void Stop()
        {
            foreach (var rule in Rules)
            {
                rule.Stop();
            }
            
            Rules.Clear();
            
            cancelTokenSource.Cancel();
        }
        
        public async void Start()
        {
            while (token.IsCancellationRequested == false)
            {
                try
                {
                    await UniTask.Delay(
                        TimeSpan.FromSeconds(checkStep), 
                        DelayType.UnscaledDeltaTime, 
                        PlayerLoopTiming.FixedUpdate, 
                        token);
                }
                catch
                {
                    return;
                }
                
                foreach (var rule in Rules)
                {
                    rule.Check();
                }

                CheckAdd();
                CheckRemove();
            }
        }
        
        private void CheckAdd()
        {
            if (addQueue.Count == 0)
            {
                return;
            }
            
            foreach (var rule in addQueue)
            {
                var ruleAdded = false;
                
                for (var index = 0; index < Rules.Count; index++)
                {
                    var r = Rules[index];
                    if (r.GetType() == rule.GetType())
                    {
                        Rules[index] = rule;
                        ruleAdded = true;
                    }
                }

                if (ruleAdded == false)
                {
                    Rules.Add(rule);
                }
            }
            
            addQueue.Clear();
        }
        
        private void CheckRemove()
        {
            if (removeQueue.Count == 0)
            {
                return;
            }

            foreach (var rule in removeQueue)
            {
                IRule ruleToRemove = null;
                
                foreach (var r in Rules)
                {
                    if (r.GetType() == rule)
                    {
                        ruleToRemove = r;
                        break;
                    }
                }
                
                Rules.Remove(ruleToRemove);
            }
            
            removeQueue.Clear();
        }
    }
}