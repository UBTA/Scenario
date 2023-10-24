using UnityEngine;

namespace FAwesome.ScenarioCore.GameFramework.Example
{
    public class ExampleInstance : Instance
    {
        [SerializeField] private Rigidbody RB;
        
        public int InstanceType;
        
        public override void Init()
        {
            base.Init();
            
            gameObject.SetActive(true);
            RB.isKinematic = false;
            
            Debug.Log("Вход в рантайм Init() " + gameObject.name);
        }

        public override void End()
        {
            Debug.Log("Выход из рантайма End() " + gameObject.name);
            
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            RB.isKinematic = true;
            
            base.End();
        }
    }
}