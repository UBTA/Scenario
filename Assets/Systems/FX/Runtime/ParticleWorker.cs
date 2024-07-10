using EblanDev.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.FXSystemUnit
{
    public class ParticleWorker : WorkerPulled<ParticleInstance>
    {
        public ParticleWorker(Transform _parent) : base(_parent)
        {
        }

        protected override bool Compare(ParticleInstance instance, ParticleInstance anotherInstance)
        {
            return instance.ID == anotherInstance.ID;
        }
    }
}