using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Workers
{
    
    /// <summary>
    /// Воркер занимается пулингом инстанов
    /// -Пулит один инстанс
    /// </summary>
    /// <typeparam name="I"></typeparam>
    public class WorkerSolo<I> where I : MonoBehaviour, IInstance
    {
        private I instance;
        
        /// <summary>
        /// Получить инстанс
        /// </summary>
        public I Get => instance;

        /// <summary>
        /// Родитель создаваемых инстансов. 
        /// </summary>
        protected Transform parent;
        
        /// <summary>
        /// Кэширует ссылку на родителя.
        /// </summary>
        /// <param name="_parent"></param>
        public WorkerSolo(Transform _parent)
        {
            parent = _parent;
        }
        
        /// <summary>
        /// Создание инстанса
        /// </summary>
        /// <param name="i"></param>
        /// <returns>
        /// Возвращает инстанс
        /// </returns>
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

        /// <summary>
        /// Очищает весь пул вне зависимости от состояние
        /// </summary>
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