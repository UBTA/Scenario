using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Movable
{
    public class SimpleRBMovable : MovableModuleBase
    {
        [SerializeField] protected Rigidbody rb;
        [Space] 
        [SerializeField] [Range(0f,1f)] protected float lookStiffness = 0.9f;
        [SerializeField] [Range(0f,1f)] protected float moveStiffness = 0.95f;
        [Space] 
        [SerializeField] protected bool useDeceleration;
        [SerializeField] protected float decelerationDist = 5f;
        [SerializeField] [Range(0f,1f)] protected float minSlowDownSpeedCoef = 0.3f;
        [Space]
        [SerializeField] protected LayerMask ground;

        protected Vector3 lastVel;

        public override void Enable()
        {
            base.Enable();
            rb.isKinematic = false;
        }

        public override void Disable()
        {
            base.Disable();
            rb.isKinematic = true;
        }

        public override void Fixed()
        {
            if (moveTargetLook)
            {
                if (moveTarget != null)
                {
                    lookTarget = moveTarget;
                    lookTargetPos = null;
                }

                if (moveTargetPos.HasValue)
                {
                    lookTargetPos = moveTargetPos;
                    lookTarget = null;
                }
            }
            
            if (movementAllowed == false)
            {
                velocity = Vector3.zero;
                rb.velocity = velocity;
                return;
            }

            look = LookDir();
            ApplyLook();
            
            if (hardLockTarget != null)
            {
                rb.transform.SetPositionAndRotation(
                    hardLockTarget.position,
                    hardLockTarget.rotation);
                velocity = Vector3.zero;
                rb.velocity = velocity;
                return;
            }

            var dir = MoveDir();
            var newVel = dir.normalized * speed;

            if (useDeceleration)
            {
                if (dir.magnitude > decelerationDist)
                {
                    velocity = Vector3.Lerp(lastVel, newVel, (1f - moveStiffness));
                }
                else
                {
                    velocity = newVel * Mathf.Clamp01((dir.magnitude / decelerationDist) + minSlowDownSpeedCoef);
                }
            }
            else
            {
                velocity = lastVel.magnitude < newVel.magnitude ? 
                    Vector3.Lerp(lastVel, newVel, (1f - moveStiffness)) :
                    newVel;
            }
            
            lastVel = velocity;

            ApplyVelocity();
        }
        
        protected virtual Vector3 MoveDir()
        {
            var direction = Vector3.zero;

            if (moveTarget != null)
            {
                direction = moveTarget.position - rb.transform.position;
            }

            if (moveTargetPos != null)
            {
                direction = moveTargetPos.Value - rb.transform.position;
            }

            if (direction.magnitude < 0.1f)
            {
                return Vector3.zero;
            }
            
            return direction;
        }
        protected virtual Vector3 LookDir()
        {
            var direction = Vector3.zero;

            if (lookTarget != null)
            {
                direction = lookTarget.position - rb.transform.position;
            }

            if (lookTargetPos != null)
            {
                direction = lookTargetPos.Value - rb.transform.position;
            }

            return direction;
        }

        protected virtual void ApplyVelocity()
        {
            if (gravity)
            {
                var ray = new Ray(rb.transform.position + Vector3.up * 0.2f, Vector3.down);

                if (Physics.Raycast(ray, out var hit, 1000f, ground))
                {
                    if (Vector3.Distance(rb.transform.position, hit.point) > 0.1f)
                    {
                        grounded = false;
                        rb.velocity = new Vector3(velocity.x, Physics.gravity.y, velocity.z).normalized * speed;
                        return;
                    }
                }

                grounded = true;
                rb.velocity = new Vector3(velocity.x, 0f, velocity.z).normalized * speed;
            }
            else
            {
                rb.velocity = velocity;
            }
        }
        
        protected virtual void ApplyLook()
        {
            if (look.normalized.magnitude > 0.0001f)
            {
                var newRot = 
                    cutLookY ? Quaternion.LookRotation(look).YRotation() : Quaternion.LookRotation(look);
                rb.transform.localRotation =
                    Quaternion.Slerp(rb.transform.localRotation, newRot, Mathf.Clamp01(1f - lookStiffness));
            }
        }
    }
}