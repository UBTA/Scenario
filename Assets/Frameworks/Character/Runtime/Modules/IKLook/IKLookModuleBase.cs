using System.Collections.Generic;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.IKLook
{
    public class IKLookModuleBase : MonoBehaviour, IIKLook
    {
        [SerializeField] protected MultiAimConstraint LookConstraint;
        [SerializeField] protected List<MultiRotationConstraint> RotConstraints;

        protected Transform TargetT;
        protected Vector3? TargetP;
        
        public virtual void Enable()
        {
            LookConstraint.weight = 1f;
            foreach (var rotConstr in RotConstraints)
            {
                rotConstr.weight = 1f;
            }
        }

        public virtual void Disable()
        {
            LookConstraint.weight = 0f;
            foreach (var rotConstr in RotConstraints)
            {
                rotConstr.weight = 0f;
            }
        }
        
        public virtual void Look(Transform target)
        {
            TargetT = target;
            TargetP = null;
        }

        public virtual void Look(Vector3? point)
        {
            TargetT = null;
            TargetP = point;
        }

        public virtual void Fixed()
        {
            if (TargetT != null)
            {
                LookConstraint.data.sourceObjects[0].transform.position = TargetT.position;
            }
            
            if (TargetP.HasValue)
            {
                LookConstraint.data.sourceObjects[0].transform.position = TargetP.Value;
            }
        }
    }
}