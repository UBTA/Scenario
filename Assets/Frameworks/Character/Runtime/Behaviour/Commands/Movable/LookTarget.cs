using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class LookTarget : Command
    {
        [SerializeField] private Transform Target;

        public LookTarget(Transform target)
        {
            Target = target;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IMovable>()?.SetLookTarget(Target);
            return UniTask.CompletedTask;
        }
    }
}