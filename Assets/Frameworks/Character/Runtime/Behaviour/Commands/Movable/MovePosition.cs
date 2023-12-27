using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class MovePosition : MoveCommand
    {
        [SerializeField] private Vector3 Position;

        public MovePosition(Vector3 movePos, bool ignoreY = false) : base(ignoreY)
        {
            Position = movePos;
        }
        
        protected override void SetTargets(Puppet puppet)
        {
            MoveTargetP = Position;
        }
    }
}