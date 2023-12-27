using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class IKLookTarget : Command
    {
        [SerializeField] private Transform LookT;

        public IKLookTarget(Transform look)
        {
            LookT = look;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IIKLook>()?.Look(LookT);
            return UniTask.CompletedTask;
        }
    }
}