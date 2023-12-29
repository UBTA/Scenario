using System;
using System.Collections.Generic;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Workers
{
    public class WorkerPulled<I> where I : MonoBehaviour, IInstance
    {
        private readonly List<I> instances = new List<I>();

        protected Transform parent;

        public WorkerPulled(Transform _parent)
        {
            parent = _parent;
        }

        public I Get(int index)
        {
            if (instances.Count > index)
            {
                return instances[index];
            }

            return null;
        }
        
        public List<I> GetAll()
        {
            return instances;
        }
        
        public I Create(I i, Action<I> OnCreated = null)
        {
            foreach (var instance in instances)
            {
                if (Compare(instance, i))
                {
                    if (instance != null)
                    {
                        if (instance.IsBusy == false)
                        {
                            OnCreated?.Invoke(instance);
                            instance.Init();
                            return instance;
                        }
                    }
                }
            }
            
            var inst = parent != null ?
                GameObject.Instantiate(i, parent) : 
                GameObject.Instantiate(i);
            
            OnCreated?.Invoke(inst);
            inst.Init();
            instances.Add(inst);
            return inst;
        }
        
        public void Clear()
        {
            foreach (var inst in instances)
            {
                inst.End();
                GameObject.Destroy(inst.gameObject);
            }
            
            instances.Clear();
        }

        protected virtual bool Compare(I instance, I anotherInstance)
        {
            return instance.GetType() == anotherInstance.GetType();
        }
    }
}