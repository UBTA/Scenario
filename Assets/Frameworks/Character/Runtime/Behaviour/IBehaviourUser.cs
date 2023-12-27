using System.Threading;
using Cysharp.Threading.Tasks;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    public interface IBehaviourUser
    {
        UniTask Behave(BehaviourTree tree, CancellationToken token = default);
        UniTask Behave(ICommand command, CancellationToken token = default);
    }
}