using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Interfaces;
using EblanDev.ScenarioCore.CharacterFramework.Modules.IKHolder;
using EblanDev.ScenarioCore.WeaponModule.Module;

namespace EblanDev.ScenarioCore.WeaponModule.Interfaces
{
    public interface IWeaponItem : IHolderItem
    {
        public void Aim(float weight);
        public UniTask Animate(IWeaponAnimation animation, WeaponGrab weaponGrab, Grab holderGrab, CancellationToken token);
    }
}