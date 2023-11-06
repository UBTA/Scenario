using System;
using FAwesome.ScenarioCore.CharacterFramework.Modules.Animatable;

namespace FAwesome.ScenarioCore.CharacterFramework.Example
{
    [Serializable]
    public class JumpOverStateExample : AnimatorState
    {
        protected override string Key => "JumpOver";
        
        public JumpOverStateExample(float duration) : base(duration)
        {
        }
    }
}