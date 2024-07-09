using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Systems;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.PopUpSystemUnit
{
    public class PupUpSystem : SystemBase
    {
        [SerializeField] private List<PopUpView> popUps;
        
        private static List<PopUpView> popUpsSt;

        public override void Prepare() {}

        public override void Init()
        {
            popUpsSt = popUps;

            foreach (var popUp in popUps)
            {
                popUp.Init();
            }
        }

        public static T Get<T>() where T : PopUpView
        {
            foreach (var popUp in popUpsSt)
            {
                if (popUp.GetType() == typeof(T))
                {
                    return (T) popUp;
                }
            }
            return null;
        }
        
        public override void Exit(bool saveData) {}
    }
}