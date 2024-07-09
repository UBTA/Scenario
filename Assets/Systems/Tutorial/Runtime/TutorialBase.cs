using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit
{
    public abstract class TutorialBase : MonoBehaviour
    {
        public Action<int> OnTutorPassed;
        
        public int ID;
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