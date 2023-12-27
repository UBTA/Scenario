using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class IKLookPosition : Command
    {
        [SerializeField] private Vector3 LookP;

        public IKLookPosition(Vector3 look)
        {
            LookP = look;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IIKLook>()?.Look(LookP);
            return UniTask.CompletedTask;
        }
    }
}