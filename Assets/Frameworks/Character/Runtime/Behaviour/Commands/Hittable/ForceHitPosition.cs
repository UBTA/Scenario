using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class ForceHitPosition : Command
    {
        [SerializeField] private Vector3 Source;

        public ForceHitPosition(Vector3 source)
        {
            Source = source;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IHittable>()?.ForceHit(Source);
            return UniTask.CompletedTask;
        }
    }
}