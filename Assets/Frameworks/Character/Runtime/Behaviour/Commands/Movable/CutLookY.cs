using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class CutLookY : Command
    {
        [SerializeField] private bool CutY;

        public CutLookY(bool cutY)
        {
            CutY = cutY;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IMovable>()?.CutLookY(CutY);
            return UniTask.CompletedTask;
        }
    }
}