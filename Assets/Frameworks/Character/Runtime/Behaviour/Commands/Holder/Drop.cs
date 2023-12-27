using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class Drop : Command
    {
        private readonly bool Left;
        private readonly bool Right;

        public Drop(bool left = true, bool right = true)
        {
            Left = left;
            Right = right;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IIKHolder>()?.Drop(Left, Right);
            return UniTask.CompletedTask;
        }
    }
}