using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class ChangeGravity : Command
    {
        [SerializeField] private bool Gravity;

        public ChangeGravity(bool gravity)
        {
            Gravity = gravity;
        }
        
        protected override async UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            var movement = puppet.Module<IMovable>();
            if (movement == null)
            {
                return;
            }
            
            movement.Gravity = Gravity;
            
            await UniTask.Delay(
                TimeSpan.FromSeconds(trashHoldTiming), 
                DelayType.UnscaledDeltaTime, 
                PlayerLoopTiming.FixedUpdate, 
                token);

            while (true)
            {
                if (!movement.IsGrounded)
                {
                    await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
                }
                else
                {
                    break;
                }
            }
        }
    }
}