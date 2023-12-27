using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework;
using EblanDev.ScenarioCore.CharacterFramework.Behaviour;
using EblanDev.ScenarioCore.WeaponModule.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.WeaponModule.Behaviour
{
    [Serializable]
    public class WeaponAimTarget : Command
    {
        [SerializeField] private Transform Target;

        public WeaponAimTarget(Transform target)
        {
            Target = target;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IWeaponUser>()?.Aim(Target);
            return UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
        }
    }
}