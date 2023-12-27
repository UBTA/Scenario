using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Interfaces
{
    public interface IMovable : ICharacterModule, IFixed
    {
        Vector3 Velocity { get; set; }
        Vector3 Look { get; set; }
        bool IsGrounded { get; }

        float Speed { set; get; }
        bool Gravity { set; get; }

        void CutLookY(bool condition);
        void MoveTargetLook();
        void SetLookTarget(Transform target);
        void SetLookTarget(Vector3? target);

        void SetHardLockMoveTarget(Transform target);
        void SetMoveTarget(Transform target);
        void SetMoveTarget(Vector3? target);
    }
}