using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using EblanDev.ScenarioCore.WeaponModule.Module;
using UnityEngine;

namespace EblanDev.ScenarioCore.WeaponModule.Interfaces
{
    public interface IWeaponUser : ICharacterModule
    {
        public WeaponGrab TwoHanded { get; }
        public WeaponGrab Right { get; }
        public WeaponGrab Left { get; }

        public void Aim(Transform target);
        public void Aim(Vector3? point);
    }
}