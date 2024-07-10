using EblanDev.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.BulletSystemUnit
{
    public class BulletWorker : WorkerPulled<BulletInstance>
    {
        public BulletWorker(Transform _parent) : base(_parent)
        {
        }

        protected override bool Compare(BulletInstance instance, BulletInstance anotherInstance)
        {
            return instance.ID == anotherInstance.ID;
        }
    }
}