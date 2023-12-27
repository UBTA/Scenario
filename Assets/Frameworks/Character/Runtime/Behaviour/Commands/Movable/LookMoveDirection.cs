using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class LookMoveDirection : Command
    {
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IMovable>()?.MoveTargetLook();
            return UniTask.CompletedTask;
        }
    }
}