using EblanDev.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.FXSystemUnit
{
    public class DecalWorker : WorkerPulled<DecalInstance>
    {
        public DecalWorker(Transform _parent) : base(_parent)
        {
        }

        protected override bool Compare(DecalInstance instance, DecalInstance anotherInstance)
        {
            return instance.TypeID == anotherInstance.TypeID;
        }
    }
}