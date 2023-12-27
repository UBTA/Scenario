using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Interfaces
{
    public interface IIKLook : ICharacterModule, IFixed
    {
        public void Look(Transform target);
        public void Look(Vector3? point);
    }
}