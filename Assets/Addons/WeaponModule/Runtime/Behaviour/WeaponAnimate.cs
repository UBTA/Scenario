using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework;
using EblanDev.ScenarioCore.CharacterFramework.Behaviour;
using EblanDev.ScenarioCore.WeaponModule.Interfaces;

namespace EblanDev.ScenarioCore.WeaponModule.Behaviour
{
    public class WeaponAnimate : Command
    {
        private IWeaponAnimation Animation;
        
        public WeaponAnimate(IWeaponAnimation anim)
        {
            Animation = anim;
        }

        protected override async UniTask ExecuteSelf(Puppet puppet, CancellationToken token)
        {
            await puppet.Module<IWeaponUser>().TwoHanded.Animate(Animation, token);
        }
    }
}