using System;
using FAwesome.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace FAwesome.ScenarioCore.GameFramework.Example
{
    [CreateAssetMenu(fileName = "ExampleData", menuName = "ScenarioFramework/Example/ExampleData", order = 1)]
    public class ExampleData : DataBase<ExamplePData>
    {
        public ExampleInstance[] Instances;
        public float SpawnDelay;
        
        public int GameSessionsCount => PData.gameSessionsCount;

        public override void Write()
        {
            PData.gameSessionsCount += 1;
            base.Write();
        }
    }

    [Serializable]
    public class ExamplePData
    {
        public int gameSessionsCount;
    }
}