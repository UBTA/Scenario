using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class MoveOffset : MoveCommand
    {
        [SerializeField] private Vector3 Offset;
        
        public MoveOffset(Vector3 offset, bool ignoreY = false) : base(ignoreY)
        {
            Offset = offset;
        }

        protected override void SetTargets(Puppet puppet)
        {
            MoveTargetP = puppet.transform.position + Offset;
        }
    }
}