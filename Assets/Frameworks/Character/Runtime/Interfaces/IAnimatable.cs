using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.CharacterFramework.Modules.Animatable;

namespace EblanDev.ScenarioCore.CharacterFramework.Interfaces
{
    public interface IAnimatable : ICharacterModule , IFixed
    {
        public UniTask Use(AnimatorState state, CancellationToken token);
    }
}