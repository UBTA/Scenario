using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.IKHolder
{
    public class IKHolderModuleBase : MonoBehaviour, IIKHolder
    {
        [SerializeField] private RigBuilder Rig;
        
        [SerializeField] protected TwoBoneIKConstraint LeftHand;
        [SerializeField] protected TwoBoneIKConstraint RightHand;
        
        [SerializeField] protected Grab LeftGrab;
        [SerializeField] protected Grab RightGrab;
        [SerializeField] protected Grab TwoHandGrab;

        public Grab Left => LeftGrab;
        public Grab Right => RightGrab;
        public Grab TwoHand => TwoHandGrab;

        public virtual void Enable()
        {
            if (TwoHandGrab.TryGetItem(out var twoHandItem))
            {
                LeftHand.weight = 1f;
                RightHand.weight = 1f;
                return;
            }

            if (LeftGrab.TryGetItem(out var leftItem))
            {
                LeftHand.weight = 1f;
            }
            
            if (RightGrab.TryGetItem(out var rightItem))
            {
                RightHand.weight = 1f;
            }
        }

        public virtual void Disable()
        {
            if (TwoHandGrab.TryGetItem(out var twoHandItem))
            {
                LeftHand.weight = 0f;
                RightHand.weight = 0f;
                return;
            }

            if (LeftGrab.TryGetItem(out var leftItem))
            {
                LeftHand.weight = 0f;
            }
            
            if (RightGrab.TryGetItem(out var rightItem))
            {
                RightHand.weight = 0f;
            }
        }
        
        public virtual void Hold(IHolderItem item, bool left = true, bool right = true)
        {
            if (left && right)
            {
                Drop(true, true);
                
                LeftHand.data.target = item.TwoHandTargets.Item1;
                RightHand.data.target = item.TwoHandTargets.Item2;
                TwoHandGrab.Use(item);

                Rig.Build();
                return;
            }

            if (left)
            {
                Drop(true, false);
                
                LeftHand.data.target = item.LeftTarget;
                LeftGrab.Use(item);
            }

            if (right)
            {
                Drop(false, true);

                RightHand.data.target = item.RightTarget;
                RightGrab.Use(item);
            }
            Rig.Build();
        }
        
        public virtual void Fixed()
        {
            TwoHandGrab.GrabUpdate();
            LeftGrab.GrabUpdate();
            RightGrab.GrabUpdate();
        }
        
        public virtual void Drop(bool left = true, bool right = true)
        {
            if (left || right)
            {
                TwoHandGrab.Drop();
                LeftHand.data.target = null;
                RightHand.data.target = null;
            }
            
            if (left)
            {
                LeftGrab.Drop();
                LeftHand.data.target = null;
            }

            if (right)
            {
                RightGrab.Drop();
                RightHand.data.target = null;
            }
            Rig.Build();
        }
    }
}