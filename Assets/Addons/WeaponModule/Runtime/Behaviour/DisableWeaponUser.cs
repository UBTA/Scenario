using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework;
using EblanDev.ScenarioCore.CharacterFramework.Behaviour;
using EblanDev.ScenarioCore.WeaponModule.Interfaces;

namespace EblanDev.ScenarioCore.WeaponModule.Behaviour
{
    [Serializable]
    public class DisableWeaponUser : Command
    {
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IWeaponUser>()?.Disable();
            return UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
        }
    }
}