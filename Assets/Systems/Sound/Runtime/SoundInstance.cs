using EblanDev.ScenarioCore.GameFramework;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.SoundSystemUnit
{
    public class SoundInstance : Instance
    {
        public SoundID ID;
        
        [SerializeField] private AudioSource source;

        public override void Init()
        {
            gameObject.SetActive(true);
            base.Init();
        }

        public void Play()
        {
            source.Play();
            StartLifeTime(source.clip.length);
        }

        public override void End()
        {
            gameObject.SetActive(false);
            base.End();
        }
    }
}