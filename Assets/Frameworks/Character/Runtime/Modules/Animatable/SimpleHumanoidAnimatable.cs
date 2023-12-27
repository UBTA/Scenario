using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Animatable
{
    public class SimpleHumanoidAnimatable : AnimatableModuleBase
    {
        [SerializeField] protected Animator animator;
        
        [SerializeField] protected string forwardKey;
        [SerializeField] protected string sideKey;

        public override void Enable()
        {
            animator.enabled = true;
        }

        public override void Disable()
        {
            animator.enabled = false;
        }

        protected override async UniTask Play(AnimatorState state, CancellationToken token)
        {
            await state.Play(animator, token);
        }

        protected override void ApplyMoveParams(Vector2 moveDir)
        {
            if (animator == null)
            {
                return;
            }
            
            animator.SetFloat(forwardKey, moveDir.y);
            animator.SetFloat(sideKey, moveDir.x);
        }
    }
}