using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Animatable
{
    [Serializable]
    public abstract class AnimatorState
    {
        [SerializeField] protected bool Automatic = true;
        
        [SerializeField] [ShowIf("@Automatic==true")] protected float Duration;

        [SerializeField] [ShowIf("@Automatic==false")] protected bool Start;
        [SerializeField] [ShowIf("@Automatic==false")] protected bool End;
        
        protected abstract string Key { get; }

        public AnimatorState(float duration)
        {
            Duration = duration;
        }
        
        public virtual async UniTask Play(Animator animator, CancellationToken token)
        {
            if (Automatic)
            {
                animator.SetBool(Key, true);

                await UniTask.Delay(
                    TimeSpan.FromSeconds(Duration),
                    DelayType.UnscaledDeltaTime, 
                    PlayerLoopTiming.FixedUpdate, 
                    token);
            
                animator.SetBool(Key, false);    
            }
            else
            {
                if (Start)
                {
                    animator.SetBool(Key, true);
                    return;
                }

                if (End)
                {
                    animator.SetBool(Key, false);
                }
            }
        }
    }
}