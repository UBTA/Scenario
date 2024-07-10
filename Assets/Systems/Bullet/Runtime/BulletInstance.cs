using EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable;
using EblanDev.ScenarioCore.GameFramework;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.BulletSystemUnit
{
    public class BulletInstance : Instance
    {
        public BulletID ID;
        
        [SerializeField] private float startSpeed;
        [SerializeField] private Rigidbody rb;

        protected bool hitRegistered;
        
        public virtual void Impulse(Vector3 start, Vector3 direction)
        {
            transform.position = start;
            transform.LookAt(start + direction);

            gameObject.SetActive(true);
            rb.isKinematic = false;
            rb.AddForce(direction.normalized * startSpeed);

            StartLifeTime();
        }
        
        protected virtual void FixedUpdate()
        {
            if (IsBusy == false)
            {
                return;
            }

            if (hitRegistered)
            {
                return;
            }

            var hits = Physics.RaycastAll(transform.position, transform.forward, startSpeed / 1000f);
            
            if (hits.Length != 0)
            {
                foreach (var result in hits)
                {
                    if (result.transform.TryGetComponent<PhysActivator>(out var resultActivator))
                    {
                        resultActivator.RegisterHit(result.point, transform.forward);
                        hitRegistered = true;
                        return;
                    }
                }
            }
        }

        public override void End()
        {
            rb.isKinematic = true;
            hitRegistered = false;
            gameObject.SetActive(false);
            base.End();
        }
    }
}