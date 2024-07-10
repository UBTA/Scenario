using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.SoundSystemUnit
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "ScenarioFramework/Systems/SoundData", order = 1)]
    public class SoundData : DataPrefs<SoundSettings>
    {
        [SerializeField] private List<SoundInstance> sounds;

        public SoundSettings Settings => PData;
        
        public SoundInstance GetSound(SoundID ID)
        {
            foreach (var sound in sounds)
            {
                if (sound.ID == ID)
                {
                    return sound;
                }
            }

            if (sounds.Count != 0)
            {
                return sounds[0];
            }

            return null;
        }
    }

    [Serializable]
    public class SoundSettings
    {
        public bool Mute = false;
    }
}