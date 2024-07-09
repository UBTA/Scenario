﻿using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit
{
    [CreateAssetMenu]
    public class TutorialData : DataPrefs<TutorialPrefs>
    {
        public void SetTutorialPassed(int tutorID)
        {
            if (PData.passedTutorialIds.TryFind((v) => tutorID == v, out var id))
            {
                return;
            }
            
            PData.passedTutorialIds.Add(tutorID);
        }

        public bool IsTutorialPassed(int tutorID)
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