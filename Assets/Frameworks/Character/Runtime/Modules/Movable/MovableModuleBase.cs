using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Movable
{
    public abstract class MovableModuleBase : MonoBehaviour, IMovable
    {
        protected Transform hardLockTarget;
        protected Transform moveTarget;
        protected Vector3? moveTargetPos;
        
        protected Transform lookTarget;
        protected Vector3? lookTargetPos;

        protected Vector3 velocity;
        protected Vector3 look;
        protected bool grounded;
       
        protected float speed;
        protected bool gravity;

        protected bool movementAllowed;
        protected bool moveTargetLook;
        protected bool cutLookY = true;

        public Vector3 Velocity
        {
            get => velocity;
            set => velocity = value;
        }
        public Vector3 Look
        {
            get => look;
            set => look = value;
        }
        
        public bool IsGrounded => grounded;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public bool Gravity
        {
            get => gravity;
            set => gravity = value; 
        }
        
        public virtual void Enable()
        {
            movementAllowed = true;
        }
        public virtual void Disable()
        {
            movementAllowed = false;
        }

        public virtual void CutLookY(bool condition)
        {
            cutLookY = condition;
        }
        public virtual void MoveTargetLook()
        {
            moveTargetLook = true;
        }
        public virtual void SetLookTarget(Transform target)
        {
            moveTargetLook = false;
            lookTarget = target;
            lookTargetPos = null;
        }
        public virtual void SetLookTarget(Vector3? target)
        {
            moveTargetLook = false;
            lookTarget = null;
            lookTargetPos = target;
        }
        
        public virtual void SetHardLockMoveTarget(Transform target)
        {
            hardLockTarget = target;
            moveTarget = null;
            moveTargetPos = null;
        }
        public virtual void SetMoveTarget(Transform target)
        {
            hardLockTarget = null;
            moveTarget = target;
            moveTargetPos = null;
        }
        public virtual void SetMoveTarget(Vector3? target)
        {
            hardLockTarget = null;
            moveTarget = null;
            moveTargetPos = target;
        }
        
        public abstract void Fixed();
    }
}