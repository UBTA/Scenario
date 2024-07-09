using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit.Tutorials
{
    public class TutorialTest : TutorialBase
    {
        private CancellationTokenSource cancelTokenSource;
        
        public async UniTask Run(List<GameObject> objects)
        {
            isActive = true;
            cancelTokenSource = new CancellationTokenSource();
            foreach (var obj in objects)
            {
                tutorialCanvas
                    .ShowHand(obj, 1f, 1f)
                    .ShowSign("Tap on Obj!")
                    .ShowShadow(obj, new Vector2(0.3f, 0.16f));

                await UniTask.Delay(2000, DelayType.UnscaledDeltaTime, PlayerLoopTiming.FixedUpdate);
            }

            OnTutorPassed.Invoke(ID);
            
            tutorialCanvas
                .HideHand()
                .HideSign()
                .HideShadow();
            
            isActive = false;
        }

        public override void Deactivate()
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();   
            }
            
            tutorialCanvas
                .HideHand()
                .HideShadow();
            
            isActive = false;
        }
    }
}