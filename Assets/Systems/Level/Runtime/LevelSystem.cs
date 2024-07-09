using EblanDev.ScenarioCore.GameFramework.Systems;
using EblanDev.ScenarioCore.GameFramework.Workers;

namespace EblanDev.ScenarioCore.Systems.LevelSystemUnit
{
    public class LevelSystem : SystemDated<LevelData>
    {
        private WorkerSolo<LevelInstance> level;

        public int LevelNumber => Data.GetLevelNumber;
        public int LevelsCount => Data.GetLevelsCount;
        public int LevelId => Data.GetLevelId;
        public int Cycle => Data.GetCycle;
        
        public LevelInstance CurrLevel => level.Get;

        public override void Init()
        {
            level = new WorkerSolo<LevelInstance>(transform);
        }

        public void SetUp()
        {
            level.Create(Data.GetLevel());
        }

        public override void Exit(bool saveData = true)
        {
            if (saveData)
            {
                Data.LevelFinished();
            }
            
            level.Clear();
            
            base.Exit(saveData);
        }
    }
}