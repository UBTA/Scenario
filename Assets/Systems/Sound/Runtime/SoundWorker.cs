using EblanDev.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.SoundSystemUnit
{
    public class SoundWorker : WorkerPulled<SoundInstance>
    {
        public SoundWorker(Transform _parent) : base(_parent)
        {
        }

        protected override bool Compare(SoundInstance instance, SoundInstance anotherInstance)
        {
            return instance.ID == anotherInstance.ID;
        }
    }
}