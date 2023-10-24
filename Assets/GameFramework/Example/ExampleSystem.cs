using System.Collections;
using FAwesome.ScenarioCore.GameFramework.Systems;
using FAwesome.ScenarioCore.GameFramework.Workers;
using UnityEngine;

namespace FAwesome.ScenarioCore.GameFramework.Example
{
    public class ExampleSystem : SystemDated<ExampleData>
    {
        
        private WorkerPulled<ExampleInstance> exampleWorker; //Стандартное использования пула, пулинг по типу
        private ExampleWorkerPulled boxedWorker; //Боксинг пулов, кастомный пулинг

        private Coroutine runtimeRoutine;

        public int GameSession => Data.GameSessionsCount;

        public override void Init()
        {
            exampleWorker = new WorkerPulled<ExampleInstance>(transform);
            boxedWorker = new ExampleWorkerPulled(transform);

            runtimeRoutine = StartCoroutine(SystemRuntimeExample());
        }

        private IEnumerator SystemRuntimeExample()
        {
            while (true)
            {
                yield return new WaitForSeconds(Data.SpawnDelay);

                foreach (var exampleInstance in Data.Instances)
                {
                    var instance = boxedWorker.Create(exampleInstance);    
                    
                    //instance.transform...
                    //instance.InstanceType
                    //instance.End();
                }
            }
        }

        public override void Exit(bool saveData = true)
        {
            if (runtimeRoutine != null)
            {
                StopCoroutine(runtimeRoutine);
            }
            
            base.Exit(saveData);
        }
    }
}