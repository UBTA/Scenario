using FAwesome.ScenarioCore.GameFramework;
using FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces;
using UnityEngine;

namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    public class ExampleWavePlacerInstance : Instance, IWavePlacerInstance
    {
        [SerializeField] private int Type;
        
        private int wave;
        public int WaveID
        {
            get => wave;
            set => wave = value;
        }

        public int GetTypeID => Type;
        
        public override void Init()
        {
            base.Init();
            gameObject.SetActive(false);
            
            Debug.Log("Вход в рантайм " + WaveID + " type " + GetTypeID);
        }

        public void Prepare()
        {
            gameObject.SetActive(true);
            Debug.Log("Prepare instance wave " + WaveID + " type " + GetTypeID);
        }

        public void Activate()
        {
            Debug.Log("Activate instance wave " + WaveID + " type " + GetTypeID);
            StartLifeTime(Random.Range(0f, 3f));
        }

        public override void End()
        {
            Debug.Log("Выход из рантайма " + WaveID + " type " + GetTypeID);
            
            gameObject.SetActive(false);
            base.End();
        }
    }
}