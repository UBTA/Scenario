using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules
{
    public abstract class DependentModule<T> : MonoBehaviour, IDependency where T : ICharacterModule
    {
        protected T Dependency;
        
        public void Resolve(Puppet puppet)
        {
            Dependency = puppet.Module<T>();
        }
    }
}