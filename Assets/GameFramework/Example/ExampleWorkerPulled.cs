using FAwesome.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace FAwesome.ScenarioCore.GameFramework.Example
{
    public class ExampleWorkerPulled : WorkerPulled<ExampleInstance>
    {
        public ExampleWorkerPulled(Transform _parent) : base(_parent)
        {
        }

        protected override bool Compare(ExampleInstance instance, ExampleInstance anotherInstance)
        {
            //Ваш метод сравнения для пулинга сущностей 
            return instance.InstanceType == anotherInstance.InstanceType;
        }
    }
}