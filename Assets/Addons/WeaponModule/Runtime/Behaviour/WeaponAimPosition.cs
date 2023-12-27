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
    public class WeaponAimPosition : Command
    {
        
        [SerializeField] private Vector3 Position;

        public WeaponAimPosition(Vector3 position)
        {
            Position = position;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IWeaponUser>()?.Aim(Position);
            return UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
        }
    }
}