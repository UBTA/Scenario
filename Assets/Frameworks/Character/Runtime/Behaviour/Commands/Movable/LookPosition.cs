using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class LookPosition : Command
    {
        [SerializeField] private Vector3 Position;

        public LookPosition(Vector3 lookPos)
        {
            Position = lookPos;
        }
        
        protected override UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            puppet.Module<IMovable>()?.SetLookTarget(Position);   
            return UniTask.CompletedTask;
        }
    }
}