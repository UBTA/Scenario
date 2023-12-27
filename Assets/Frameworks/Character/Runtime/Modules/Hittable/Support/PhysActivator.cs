using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable
{
    public class PhysActivator : MonoBehaviour
    {
        public event Action<Hit> OnHit;
        
        public Collider Col;
        public Rigidbody RB;
        public string Part;

        public virtual bool Kinematic
        {
            set
            {
                Col.isTrigger = value;
                RB.isKinematic = value;
                RB.velocity = Vector3.zero;
                RB.angularVelocity = Vector3.zero;
            }
        }

        public virtual void RegisterHit(Vector3 pos, Vector3 dir)
        {
            OnHit?.Invoke(new Hit(this, pos, dir));
        }
    }

    
}