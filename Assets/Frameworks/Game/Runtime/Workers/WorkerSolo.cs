using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Workers
{
    public class WorkerSolo<I> where I : MonoBehaviour, IInstance
    {
        protected I instance;
        protected Transform parent;
        
        public I Get => instance;
        
        public WorkerSolo(Transform _parent)
        {
            parent = _parent;
        }
        
        public I Create(I i)
        {
            if (instance != null)
            {
                instance.Init();
                return instance;
            }
            
            var inst = parent != null ?
                GameObject.Instantiate(i, parent) : 
                GameObject.Instantiate(i);
            
            inst.Init();
            instance = inst;
            return inst;
        }

        public void Clear()
        {
            if (instance != null)
            {
                instance.End();
                GameObject.Destroy(instance.gameObject);
                instance = null;
            }
        }
    }
}