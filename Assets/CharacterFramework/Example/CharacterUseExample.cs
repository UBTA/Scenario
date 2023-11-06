using FAwesome.ScenarioCore.CharacterFramework.Behaviour;
using UnityEngine;

namespace FAwesome.ScenarioCore.CharacterFramework.Example
{
    public class CharacterUseExample : MonoBehaviour
    {
        [SerializeField] private Character character;
        
        public void Start()
        {
            character.Init();
            
            var commands = 
                new EnableMovement{Await = false}
                    .Add(new ChangeSpeed(3f){Await = false})
                    .Add(new CutLookY(true){Await = false})
                    .Add(new LookMoveDirection{Await = false})
                    .Add(new ChangeGravity(true))
                    .Add(new Loop(-1)
                        .Add(new MovePosition(new Vector3(0f, 0f, 10f), true))
                        .Add(new MovePosition(new Vector3(0f, 0f, 0f), true)));

            character.Behave(commands);
        }
    }
}