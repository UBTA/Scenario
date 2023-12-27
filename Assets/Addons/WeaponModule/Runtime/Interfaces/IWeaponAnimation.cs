using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Modules.IKHolder;
using EblanDev.ScenarioCore.WeaponModule.Module;

namespace EblanDev.ScenarioCore.WeaponModule.Interfaces
{
    public interface IWeaponAnimation
    {
        public UniTask Use(IWeaponItem weapon, WeaponGrab weaponGrab, Grab holderGrab, CancellationToken token);
    }
}