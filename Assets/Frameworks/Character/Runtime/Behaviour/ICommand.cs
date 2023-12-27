using System.Threading;
using Cysharp.Threading.Tasks;

namespace EblanDev.ScenarioCore.CharacterFramework.Behaviour
{
    public interface ICommand
    {
        public bool Await { get; set; }

        UniTask Execute(Puppet puppet, CancellationToken token);
    }
}