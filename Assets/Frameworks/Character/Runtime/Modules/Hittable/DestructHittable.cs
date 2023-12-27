using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable
{
    public class DestructHittable : HittableModuleBase
    {
        [SerializeField] protected float impactForce = 10000;
        
        public override void Enable()
        {
            foreach (var activator in activators)
            {
                activator.OnHit += CatchHit;
                activator.Kinematic = true;
            }
        }

        public override void Disable()
        {
            foreach (var activator in activators)
            {
                activator.Kinematic = false;
                activator.OnHit -= CatchHit;
                
                if (LastHit != null)
                {
                    activator.RB.AddForce(LastHit.Direction.normalized * impactForce);
                }
            }
        }
    }
}