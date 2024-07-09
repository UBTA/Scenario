using EblanDev.ScenarioCore.GameFramework;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.LevelSystemUnit
{
    public class LevelInstance : Instance
    {
        [SerializeField] private bool isHardLevel;

        public bool IsHardLevel => isHardLevel;
    }
}