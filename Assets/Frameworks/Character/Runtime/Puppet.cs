using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using EblanDev.ScenarioCore.CharacterFramework.Modules;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework
{
    public class Puppet
    {
        public readonly Transform transform;

        private readonly Dictionary<Type, ICharacterModule> Modules;

        private readonly List<IFixed> FixedModules;
        private readonly List<ILate> LateModules;
        
        public Puppet(Transform parent)
        {
            transform = parent;
            var charModules = transform.GetComponentsInChildren<ICharacterModule>();

            Modules = new Dictionary<Type, ICharacterModule>();
            FixedModules = new List<IFixed>();
            LateModules = new List<ILate>();

            foreach (var module in charModules)
            {
                Modules.Add(module.GetType(), module);
                if (module is IFixed fix)
                {
                    FixedModules.Add(fix);
                }
                
                if (module is ILate late)
                {
                    LateModules.Add(late);
                }
            }

            foreach (var module in charModules)
            {
                if (module is IDependency dependency)
                {
                    dependency.Resolve(this);
                }
            }
        }

        public T Module<T>() where T : ICharacterModule
        {
            var res = Modules.TryGetValue(typeof(T), out var mod);

            if (res == false)
            {
                foreach (var module in Modules)
                {
                    if (module.Value is T value)
                    {
                        Modules.Add(typeof(T), value);
                        return value;
                    }
                }
            }
            
            if (mod != null)
            {
                return (T)mod;
            }

            return default;
        }

        public void Fixed()
        {
            foreach (var mod in FixedModules)
            {
                mod.Fixed();
            }
        }

        public void Late()
        {
            foreach (var mod in LateModules)
            {
                mod.Late();
            }
        }
    }
}