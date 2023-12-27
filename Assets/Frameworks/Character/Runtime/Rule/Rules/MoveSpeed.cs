using System;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Rules
{
    [Serializable]
    public class MoveSpeed : IRule
    {
        [SerializeField] private float Speed;
        
        private IMovable Movement;

        public MoveSpeed(float speed)
        {
            Speed = speed;
        }
        
        public void Start(Puppet puppet, RuleBook rules)
        {
            Movement = puppet.Module<IMovable>();

            if (Movement == null)
            {
                return;
            }

            Movement.Speed = Speed;
        }

        public void Check(){}

        public void Stop()
        {
            if (Movement == null)
            {
                return;
            }
            
            Movement.Speed = 0f;
        }
    }
}