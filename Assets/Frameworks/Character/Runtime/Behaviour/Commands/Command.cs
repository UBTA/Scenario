using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public abstract class Command : ICommand
    {
        [SerializeField] protected bool IsAwaited = false;
        
        protected readonly List<ICommand> commands = new List<ICommand>();
        protected float trashHoldTiming = 0.1f;
        
        public bool Await
        {
            get => IsAwaited;
            set => IsAwaited = value;
        }

        public virtual Command Add(ICommand command)
        {
            commands.Add(command);
            return this;
        }


        public virtual async UniTask Execute(Puppet puppet, CancellationToken token)
        {
            if (Await)
            {
                await ExecuteSelf(puppet, token);
            }
            else
            {
                ExecuteSelf(puppet, token).Forget();
            }

            if (commands == null)
            {
                return;
            }

            foreach (var command in commands)
            {
                if (command.Await)
                {
                    await command.Execute(puppet, token);
                }
                else
                {
                    command.Execute(puppet, token).Forget();
                }
            }
        }

        protected abstract UniTask ExecuteSelf(Puppet puppet, CancellationToken token);
    }
}