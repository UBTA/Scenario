using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit
{
    public abstract class TutorialBase : MonoBehaviour
    {
        public Action<TutorialID> OnTutorPassed;
        
        public TutorialID ID;
        public bool IsActive => isActive;

        protected TutorialCanvas tutorialCanvas;
        protected bool isActive;

        public void Init(TutorialCanvas canvas)
        {
            tutorialCanvas = canvas;
        }
        public abstract void Deactivate();
    }
}