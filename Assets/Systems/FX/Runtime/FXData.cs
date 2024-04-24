using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.FXSystemUnit
{
    [CreateAssetMenu(fileName = "FXData", menuName = "ScenarioFramework/Systems/FXData", order = 1)]
    public class FXData : DataClear
    {
        [SerializeField] private List<ParticleInstance> particles;

        public ParticleInstance GetParticle(int id)
        {
            foreach (var particle in particles)
            {
                if (particle.TypeID == id)
                {
                    return particle;
                }
            }

            if (particles.Count != 0)
            {
                return particles[0];
            }

            return null;
        }
    }
}