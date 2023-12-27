using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    [Serializable]
    public class BehaviourTree
    {
        [TypeFilter("FilterCommands")] public List<ICommand> Commands;

        public IEnumerable<Type> FilterCommands()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> result = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (typeof(ICommand).IsAssignableFrom(type))
                    {
                        result.Add(type);
                    }
                }
            }

            return result;
        }
    }
}