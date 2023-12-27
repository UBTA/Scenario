using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class MoveTarget : MoveCommand
    {
        [SerializeField] private Transform Target;

        public MoveTarget(Transform target, bool ignoreY = false) : base(ignoreY)
        {
            Target = target;
        }

        protected override void SetTargets(Puppet puppet)
        {
            MoveTargetT = Target;
        }
    }
}