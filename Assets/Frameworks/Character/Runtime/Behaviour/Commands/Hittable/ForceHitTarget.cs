using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class ForceHitTarget : Command
    {
        [SerializeField] private Transform Source;

        public ForceHitTarget(Transform source)
        {
            Source = source;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IHittable>()?.ForceHit(Source.position);
            return UniTask.CompletedTask;
        }
    }
}