using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using EblanDev.ScenarioCore.CharacterFramework.Modules;
using EblanDev.ScenarioCore.WeaponModule.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.WeaponModule.Module
{
    public class WeaponUserModuleBase : DependentModule<IIKHolder>, IWeaponUser
    {
        [SerializeField] private WeaponGrab TwoHandedGrab;
        [SerializeField] private WeaponGrab RightGrab;
        [SerializeField] private WeaponGrab LeftGrab;
        
        public WeaponGrab TwoHanded => TwoHandedGrab;
        public WeaponGrab Right => RightGrab;
        public WeaponGrab Left => LeftGrab;
        
        public void Enable()
        {
            TwoHandedGrab.Connect(Dependency.TwoHand);
            LeftGrab.Connect(Dependency.Left);
            RightGrab.Connect(Dependency.Right);
        }

        public void Disable()
        {
            TwoHandedGrab.Disconnect();
            LeftGrab.Disconnect();
            RightGrab.Disconnect();
        }
        
        public void Aim(Transform target)
        {
            TwoHandedGrab.TargetT = target;
            TwoHandedGrab.TargetP = null;
            LeftGrab.TargetT = target;
            LeftGrab.TargetP = null;
            RightGrab.TargetT = target;
            RightGrab.TargetP = null;
        }
        
        public void Aim(Vector3? point)
        {
            TwoHandedGrab.TargetT = null;
            TwoHandedGrab.TargetP = point;
            LeftGrab.TargetT = null;
            LeftGrab.TargetP = point;
            RightGrab.TargetT = null;
            RightGrab.TargetP = point;
        }
    }
}