using System;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Data
{
    public class DataPrefs<P> : ScriptableObject, IData
    {
        [SerializeField] private bool UseInspectorData;
        [SerializeField] protected P PData;
        
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

        public virtual void Write()
        {
            string value = JsonUtility.ToJson(PData);
            PlayerPrefs.SetString(typeof(P).ToString(), value);
        }
    }
}