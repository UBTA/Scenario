using System;
using EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable;
using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Interfaces
{
    public interface IHittable : ICharacterModule
    {
        public event Action<Hit> OnHit;
        
        public void ForceHit(Vector3? source = null);
    }
}