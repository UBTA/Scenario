using System;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.IKHolder
{
    [Serializable]
    public class Grab
    {
        public event Action OnUse;
        public event Action OnPositioned;
        public event Action OnDrop;
        
        public Transform target;
        public Vector3 offset;
        public Vector3 angles;

        [NonSerialized] public bool overridePos;

        protected IHolderItem item;
        
        public virtual bool TryGetItem(out IHolderItem result)
        {
            if (item != null)
            {
                result = item;
                return true;
            }

            result = null;
            return false;
        }
        
        public virtual void Use(IHolderItem itemRoot)
        {
            item = itemRoot;
            item.OnGrab();
            
            OnUse?.Invoke();
        }

        public virtual void GrabUpdate()
        {
            if (item == null)
            {
                return;
            }

            item.Self.position = target.position;
            item.Self.rotation = target.rotation;
            
            if (overridePos == false)
            {
                item.AimingRoot.localPosition = offset;
                item.AimingRoot.localRotation = Quaternion.Euler(angles);
            }
            else
            {
                OnPositioned?.Invoke();   
            }
        }

        public virtual void Drop()
        {
            if (item != null)
            {
                item.OnDrop();
                item = null;
                
                OnDrop?.Invoke();
            }
        }
    }
}