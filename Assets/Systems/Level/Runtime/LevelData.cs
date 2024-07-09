using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.LevelSystemUnit
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScenarioFramework/Systems/LevelData", order = 1)]
    public class LevelData : DataPrefs<LevelP>
    {
        [SerializeField] private int ignoreOnCycleFirstLevelsCount = 5;
        [SerializeField] private List<LevelInstance> levels;

        public int GetLevelNumber => GetCycle * GetLevelsCount + 1 + GetLevelId;
        public int GetLevelsCount => levels.Count;
        public int GetLevelId => PData.currLevelID;
        public int GetCycle => PData.currCycle;
        
        public LevelInstance GetLevel()
        {
            if (GetCycle == 0)
            {
                Debug.Log("Loaded level ID : " + PData.currLevelID);
                return levels[PData.currLevelID];
            }

            if (levels[PData.currLevelID].IsHardLevel)
            {
                var hardLevels = levels.FindAll(v => v.IsHardLevel);
                if (hardLevels.Count != 0)
                {
                    var rndHardLevel = hardLevels.GetRandom();
                    PData.rndLevelID = levels.IndexOf(rndHardLevel);
                    Debug.Log("Loaded random hard level ID : " + PData.rndLevelID);
                    return levels[PData.rndLevelID];
                }
            }

            Debug.Log("Loaded random level ID : " + PData.rndLevelID);
            return levels[PData.rndLevelID];
        }

        public void LevelFinished()
        {
            PData.currLevelID++;
            if (PData.currLevelID >= levels.Count)
            {
                PData.currCycle++;
                PData.currLevelID = 0;
                PData.rndLevelID = UnityEngine.Random.Range(ignoreOnCycleFirstLevelsCount + 1, levels.Count);
            }
            PData.rndLevelID = UnityEngine.Random.Range(ignoreOnCycleFirstLevelsCount + 1, levels.Count);
        }
    }
    
    [Serializable]
    public class LevelP
    {
        public int currCycle;
        public int currLevelID;
        public int rndLevelID;
    }
}