using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Modules.IKHolder;
using EblanDev.ScenarioCore.WeaponModule.Interfaces;
using EblanDev.ScenarioCore.WeaponModule.Module;
using UnityEngine;

namespace EblanDev.ScenarioCore.WeaponModule.Weapon
{
    public abstract class WeaponItemBase : MonoBehaviour, IWeaponItem
    {
        [SerializeField] protected Transform aimRoot;
        [SerializeField] protected Transform view;
        [Space]
        [SerializeField] protected Transform left;
        [SerializeField] protected Transform right;
        [Space]
        [SerializeField] protected Transform leftTwoHanded;
        [SerializeField] protected Transform rightTwoHanded;
        
        public Transform LeftTarget => left;
        public Transform RightTarget => right;
        public (Transform, Transform) TwoHandTargets => (leftTwoHanded, rightTwoHanded);
        public Transform Self => transform;
        public Transform AimingRoot => aimRoot;
        public Transform ViewRoot => view;

        public abstract void OnGrab();

        public abstract void OnDrop();

        public abstract void Aim(float weight);
        public async UniTask Animate(IWeaponAnimation animation, 
            WeaponGrab weaponGrab, 
            Grab holderGrab, 
            CancellationToken token)
        {
            await animation.Use(this, weaponGrab, holderGrab, token);
        }
    }
}