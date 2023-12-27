using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using EblanDev.ScenarioCore.CharacterFramework.Modules.Animatable;
using Sirenix.OdinInspector;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    public class Animate : Command
    {
        [TypeFilter("FilterStates")] public AnimatorState state;
        
        protected override async UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            var animator = puppet.Module<IAnimatable>();
            
            if (animator == null)
            {
                return;
            }

            await animator.Use(state, token);
        }
        
        public IEnumerable<Type> FilterStates()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> result = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (typeof(AnimatorState).IsAssignableFrom(type))
                    {
                        result.Add(type);
                    }
                }
            }

            return result;
        }
    }
}