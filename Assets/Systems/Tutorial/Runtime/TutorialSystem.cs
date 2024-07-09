using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Systems;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit
{
    public class TutorialSystem : StaticSystemDated<TutorialData>
    {
        [SerializeField] private List<TutorialBase> tutorials;
        [SerializeField] private TutorialCanvas tutorialCanvas;

        private static List<TutorialBase> tutorialsSt;

        public override void Init()
        {
            tutorialCanvas.Init();
            
            foreach (var tutorial in tutorials)
            {
                tutorial.Init(tutorialCanvas);
                tutorial.OnTutorPassed += Data.SetTutorialPassed;
            }

            tutorialsSt = tutorials;
        }

        public static bool TryGetTutorial<T>(out T tutor) where T : TutorialBase
        {
            foreach (var tutorial in tutorialsSt)
            {
                if (tutorial is T tutorialT)
                {
                    if (DataStatic.IsTutorialPassed(tutorial.ID) == false)
                    {
                        tutor = tutorialT;
                        return true;
                    }
                }
            }

            tutor = null;
            return false;
        }

        public override void Exit(bool saveData = true)
        {
            base.Exit(true);
        }
    }
}