using EblanDev.ScenarioCore.GameFramework.Systems;

namespace EblanDev.ScenarioCore.Systems.SoundSystemUnit
{
    public class SoundSystem : StaticSystemDated<SoundData>
    {
        private static SoundWorker sounds;
        
        public static SoundSettings Settings => DataStatic.Settings;
        
        public override void Init()
        {
            sounds = new SoundWorker(transform);
        }

        public static void Sound(int id)
        {
            if (DataStatic.Settings.Mute)
            {
                if (sounds.GetAll().Count != 0)
                {
                    sounds.Clear();
                }

                return;
            }
            
            var sound = sounds.Create(DataStatic.GetSound(id));
            sound.Play();
        }
    }
}