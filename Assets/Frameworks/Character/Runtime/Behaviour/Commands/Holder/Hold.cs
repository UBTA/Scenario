using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class Hold : Command
    {
        private readonly IHolderItem Item;

        private readonly bool Left;
        private readonly bool Right;

        public Hold(IHolderItem item, bool left = true, bool right = true)
        {
            Item = item;

            Left = left;
            Right = right;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IIKHolder>()?.Hold(Item, Left, Right);
            return UniTask.CompletedTask;
        }
    }
}