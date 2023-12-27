using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public abstract class MoveCommand : Command
    {
        [SerializeField] protected bool IgnoreY;
        
        protected Vector3? MoveTargetP;
        protected Transform MoveTargetT;

        protected IMovable Movement;

        public MoveCommand(bool ignoreY = false)
        {
            IgnoreY = ignoreY;
        }

        protected override async UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            Movement = puppet.Module<IMovable>();
            if (Movement == null)
            {
                return;
            }

            SetTargets(puppet);
            
            if (MoveTargetT != null)
            {
                Movement.SetMoveTarget(MoveTargetT);
            }
            else
            {
                if (!MoveTargetP.HasValue)
                {
                    return;
                }
                
                Movement.SetMoveTarget(MoveTargetP);
            }

            await UniTask.Delay(
                TimeSpan.FromSeconds(trashHoldTiming), 
                DelayType.UnscaledDeltaTime, 
                PlayerLoopTiming.FixedUpdate);
            
            while (true)
            {
                if (IgnoreY)
                {
                    var currP = new Vector3(
                        puppet.transform.position.x, 
                        0f, 
                        puppet.transform.position.z);
                    
                    var targetP = new Vector3(
                        MoveTargetT != null ? MoveTargetT.transform.position.x : MoveTargetP.Value.x, 
                        0f, 
                        MoveTargetT != null ? MoveTargetT.transform.position.z : MoveTargetP.Value.z);
                    
                    var dist = Vector3.Distance(currP, targetP);
                    
                    if (dist >= 0.1f)
                    {
                        await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
                        
                        if (token.IsCancellationRequested)
                        {
                            Movement.SetMoveTarget((Vector3?)null);
                            Movement.SetMoveTarget((Transform)null);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (Movement.Velocity.magnitude >= 0.0001f)
                    {
                        await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
                        
                        if (token.IsCancellationRequested)
                        {
                            Movement.SetMoveTarget((Vector3?)null);
                            Movement.SetMoveTarget((Transform)null);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        protected abstract void SetTargets(Puppet puppet);
    }
}