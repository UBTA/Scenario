using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Data
{
    /// <summary>
    /// Используй UseInspectorData для входа в рантайм с объектом типа P из инспектора.
    /// </summary>
    /// <typeparam name="P">
    ///  Тип сохраняемый в PlayerPrefs.
    /// </typeparam>
    public class DataPrefs<P> : ScriptableObject, IData
    {
        [SerializeField] private bool UseInspectorData;
        
        /// <summary>
        /// Экземпляр типа P.
        /// </summary>
        [SerializeField] protected P PData;
        
        /// <summary>
        /// Подготовка даты к рантайму.
        /// Считывание даты типа P из PlayerPrefs и сериализация ее в рантайм.
        /// </summary>
        public virtual void Read()
        {
            if (UseInspectorData)
            {
                return;
            }

            if (PlayerPrefs.HasKey(typeof(P).ToString()))
            {
                string value = PlayerPrefs.GetString(typeof(P).ToString());
                PData = JsonUtility.FromJson<P>(value);
            }
            else
            {
                PData = (P) Activator.CreateInstance(typeof(P));
            }
        }

        /// <summary>
        /// Выход даты из рантайма.
        /// Запись даты типа P в PlayerPrefs из рантайма.
        /// </summary>
        public virtual void Write()
        {
            string value = JsonUtility.ToJson(PData);
            PlayerPrefs.SetString(typeof(P).ToString(), value);
        }
    }
}