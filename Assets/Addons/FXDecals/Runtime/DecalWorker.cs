using EblanDev.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace EblanDev.ScenarioCore.FXDecals
{
    public class DecalWorker : WorkerPulled<DecalInstance>
    {
        public DecalWorker(Transform _parent) : base(_parent)
        {
        }

        protected override bool Compare(DecalInstance instance, DecalInstance anotherInstance)
        {
            return instance.ID == anotherInstance.ID;
        }
    }
}