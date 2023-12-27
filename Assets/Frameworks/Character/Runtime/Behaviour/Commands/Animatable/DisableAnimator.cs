using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class DisableAnimator : Command
    {
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IAnimatable>()?.Disable();
            return UniTask.CompletedTask;
        }
    }
}