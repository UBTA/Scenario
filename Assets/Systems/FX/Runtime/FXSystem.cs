using EblanDev.ScenarioCore.GameFramework.Systems;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.FXSystemUnit
{
    public class FXSystem : StaticSystemDated<FXData>
    {
        private static ParticleWorker particles;
        private static DecalWorker decals;

        private static Transform thisT;
        
        
        public override void Init()
        {
            particles = new ParticleWorker(transform);
            decals = new DecalWorker(transform);
            thisT = transform;
        }

        public static void Particle(int id, Vector3 pos, Vector3 dir, Transform parent = null)
        {
            var particle = particles.Create(DataStatic.GetParticle(id));

            particle.transform.position = pos;
            particle.transform.rotation = Quaternion.LookRotation(-dir);

            if (parent != null)
            {
                particle.ParentTo(parent, thisT);
            }

            particle.Play();
        } 
        
        public static void Decal(int id, Vector3 pos,  Vector3 dir, Transform parent = null)
        {
            var decal = decals.Create(DataStatic.GetDecal(id));

            decal.transform.position = pos;

            if (parent != null)
            {
                decal.ParentTo(parent, thisT);
            }

            decal.Place(dir);
        } 
    }
}