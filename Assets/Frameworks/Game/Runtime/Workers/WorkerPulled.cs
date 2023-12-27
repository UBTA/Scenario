using System;
using System.Collections.Generic;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Workers
{
    
    /// <summary>
    /// Воркер занимается пулингом инстанов.
    /// Пулит множество инстансов типа I 
    /// </summary>
    /// <typeparam name="I"></typeparam>
    public class WorkerPulled<I> where I : MonoBehaviour, IInstance
    {
        private readonly List<I> instances = new List<I>();

        /// <summary>
        /// Родитель создаваемых инстансов. 
        /// </summary>
        protected Transform parent;

        /// <summary>
        /// Кэширует ссылку на родителя.
        /// </summary>
        /// <param name="_parent"></param>
        public WorkerPulled(Transform _parent)
        {
            parent = _parent;
        }

        /// <summary>
        /// Получить инстанс
        /// </summary>
        /// <param name="index">
        /// Индекс в порядке первого создания инстанса (не обновляется в процессе рааботты пула)
        /// </param>
        /// <returns>
        /// Возвращает инстанс или нул
        /// </returns>
        public I Get(int index)
        {
            if (instances.Count > index)
            {
                return instances[index];
            }

            return null;
        }

        
        /// <summary>
        /// Получить все инстансы в пуле
        /// </summary>
        /// <returns>
        /// Возвращается массив инстансов вне зависимостти от их текущего состояния
        /// </returns>
        public List<I> GetAll()
        {
            return instances;
        }
        
        /// <summary>
        /// Создание инстанса
        /// </summary>
        /// <param name="i"></param>
        /// <param name="OnCreated"></param>
        /// <returns>
        /// Возвращает инстанс
        /// </returns>
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
        
        
        /// <summary>
        /// Очищает весь пул вне зависимости от состояние
        /// </summary>
        public void Clear()
        {
            foreach (var inst in instances)
            {
                inst.End();
                GameObject.Destroy(inst.gameObject);
            }
            
            instances.Clear();
        }

        
        /// <summary>
        /// Процесс сравнения инстансов для пуллинга (базово сравнивает типы)
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="anotherInstance"></param>
        /// <returns></returns>
        protected virtual bool Compare(I instance, I anotherInstance)
        {
            return instance.GetType() == anotherInstance.GetType();
        }
    }
}