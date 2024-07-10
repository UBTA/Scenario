using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit
{
    [CreateAssetMenu(fileName = "TutorialData", menuName = "ScenarioFramework/Systems/TutorialData", order = 1)]
    public class TutorialData : DataPrefs<TutorialPrefs>
    {
        public void SetTutorialPassed(TutorialID tutorID)
        {
            if (PData.passedTutorialIds.TryFind((v) => tutorID == v, out var id))
            {
                return;
            }
            
            PData.passedTutorialIds.Add(tutorID);
        }

        public bool IsTutorialPassed(TutorialID tutorID)
        {
            return PData.passedTutorialIds.TryFind((v) => tutorID == v, out var id);
        }
    }

    [Serializable]
    public class TutorialPrefs
    {
        public List<int> passedTutorialIds;

        public TutorialPrefs()
        {
            passedTutorialIds = new List<int>();
        }
    }
}