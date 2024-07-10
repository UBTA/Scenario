using EblanDev.ScenarioCore.GameFramework.Systems;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.FXSystemUnit
{
    public class FXSystem : StaticSystemDated<FXData>
    {
        private static ParticleWorker particles;
        
        protected static Transform thisT;
        
        public override void Init()
        {
            particles = new ParticleWorker(transform);
            thisT = transform;
        }

        public static void Particle(FXID ID, Vector3 pos, Vector3 dir, Transform parent = null)
        {
            var particle = particles.Create(DataStatic.GetParticle(ID));

            particle.transform.position = pos;
            particle.transform.rotation = Quaternion.LookRotation(-dir);

            if (parent != null)
            {
                particle.ParentTo(parent, thisT);
            }

            particle.Play();
        }
    }
}