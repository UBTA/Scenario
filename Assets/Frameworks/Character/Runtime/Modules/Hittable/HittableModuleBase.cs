using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable
{
    public abstract class HittableModuleBase : MonoBehaviour, IHittable
    {
        public event Action<Hit> OnHit;

        [SerializeField] protected List<PhysActivator> activators;
        [SerializeField] protected PhysActivator fallBack;

        protected Hit LastHit;

        public abstract void Enable();
        public abstract void Disable();
        
        public virtual void ForceHit(Vector3? source = null)
        {
            Hit hit = new Hit
            {
                Activator = fallBack,
                Point = fallBack.transform.position,
                Direction = -fallBack.transform.forward
            };

            if (source != null)
            {
                hit.Direction = hit.Point - source.Value;
            }
            
            CatchHit(hit);
        }
        
        protected virtual void CatchHit(Hit hit)
        {
            LastHit = hit;
            OnHit?.Invoke(hit);
        }
    }
}