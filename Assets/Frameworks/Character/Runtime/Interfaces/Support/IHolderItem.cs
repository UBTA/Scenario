using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Interfaces
{
    public interface IHolderItem
    {
        public Transform LeftTarget { get; }
        public Transform RightTarget { get; }
        public (Transform, Transform) TwoHandTargets { get; }

        public Transform Self { get; }
        public Transform AimingRoot { get; }
        public Transform ViewRoot { get; }

        public void OnGrab();
        public void OnDrop();
    }
}