using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Modules.IKHolder;
using EblanDev.ScenarioCore.WeaponModule.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.WeaponModule.Module
{
    [Serializable]
    public class WeaponGrab
    {
        public float distance;
        public Vector3 aimOffset;

        private IWeaponItem weapon;
        private Grab currGrab;

        public float Weight;
        
        [NonSerialized] public Transform TargetT;
        [NonSerialized] public Vector3? TargetP;

        public void Connect(Grab grab)
        {
            currGrab = grab;
            currGrab.OnUse += GrabUse;
            currGrab.OnDrop += GrabDrop;
            currGrab.OnPositioned += OverrideGrab;
            currGrab.overridePos = true;
        }

        public async UniTask Animate(IWeaponAnimation animation, CancellationToken token)
        {
            if (weapon != null)
            {
                await weapon.Animate(animation, this, currGrab, token);
            }
        }
        
        private void GrabUse()
        {
            if (currGrab.TryGetItem(out var item))
            {
                if (item is IWeaponItem result)
                {
                    weapon = result;
                }
            }
        }

        private void OverrideGrab()
        {
            if (weapon != null)
            {
                if (TargetT == null && !TargetP.HasValue)
                {
                    return;
                }

                Vector3 target = Vector3.zero;

                if (TargetT != null)
                {
                    target = TargetT.position;
                }

                if (TargetP.HasValue)
                {
                    target = TargetP.Value;
                }

                weapon.Aim(Weight);
                
                //DISTANCE FROM SHOULDER AIMING

                weapon.AimingRoot.localPosition = currGrab.offset;
                var grabWorld = weapon.AimingRoot.position;

                weapon.AimingRoot.localPosition = aimOffset;
                var aimWorld = weapon.AimingRoot.position;
                var aimWorldFinal = (target - aimWorld).normalized * distance + aimWorld;

                weapon.AimingRoot.position = Vector3.Lerp(grabWorld, aimWorldFinal, Weight);

                weapon.AimingRoot.localRotation = Quaternion.Euler(currGrab.angles);
                
                var grabRot = weapon.AimingRoot.rotation;
                var aimRot = Quaternion.LookRotation((target - aimWorld).normalized, currGrab.target.up);

                weapon.AimingRoot.rotation = Quaternion.Lerp(grabRot, aimRot, Weight);
            }
        }

        private void GrabDrop()
        {
            weapon = null;
        }

        public void Disconnect()
        {
            currGrab.OnUse -= GrabUse;
            currGrab.OnDrop -= GrabDrop;
            currGrab.OnPositioned -= OverrideGrab;
            currGrab = null;
            weapon = null;
            currGrab.overridePos = false;
        }
    }
}