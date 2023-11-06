using FAwesome.ScenarioCore.CharacterFramework.Behaviour;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FAwesome.ScenarioCore.CharacterFramework.Example
{
    public class CharacterUsabilityExample : MonoBehaviour
    {
        [SerializeField] private BehaviourTree tree;
        [SerializeField] private Character character;

        public void Awake()
        {
            character.Init();
        }

        [Button]
        public void UseTree()
        {
            character.Behave(tree);
        }

        [Button]
        public void UseScripted()
        {
            var commands = 
                new ChangeGravity(true){Await = false}
                .Add(new ChangeSpeed(3f){Await = false})
                .Add(new CutLookY(true){Await = false})
                .Add(new LookMoveDirection{Await = false})
                .Add(new EnableMovement{Await = false})
                .Add(new Loop(-1)
                    .Add(new MovePosition(new Vector3(0f, 0f, 10f), true))
                    .Add(new MovePosition(new Vector3(0f, 0f, 0f), true)));

            character.Behave(commands);
        }
        
    }
}