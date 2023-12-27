using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Animatable
{
    public abstract class AnimatableModuleBase : DependentModule<IMovable>, IAnimatable
    {
        public abstract void Enable();
        public abstract void Disable();

        public virtual void Fixed()
        {
            var velocity = Dependency.Velocity;
            var look = Dependency.Look;
            
            var result = new Vector2();
            
            if (velocity.magnitude > 0.0001f)
            {
                var dot = Vector3.Dot(velocity.normalized, look.normalized);
                var projVel = new Vector2(velocity.normalized.x, velocity.normalized.z);
                var projLook = new Vector2(look.x, look.z);
                var sign = 1f;
                
                if (projVel.x < projLook.x)
                {
                    sign = -1f;
                }
                
                result.y = dot;
                result.x = sign * (1 - Mathf.Abs(dot));
                
                result = result * velocity.magnitude;
            }

            ApplyMoveParams(result);
        }
        
        public async UniTask Use(AnimatorState state, CancellationToken token)
        {
            await Play(state, token);
        }

        protected abstract UniTask Play(AnimatorState state, CancellationToken token);
        protected abstract void ApplyMoveParams(Vector2 moveDir);
    }
}