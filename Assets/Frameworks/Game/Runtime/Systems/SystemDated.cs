using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{
    public abstract class SystemDated<D> : SystemBase where D : IData
    {
        [SerializeField] protected D Data;

        public override bool TryGetData(out IData data)
        {
            data = null;
            
            if (Data == null)
            {
                return false;
            }

            data = Data;
            return true;
        }
        
        public override void Prepare()
        {
            if (Data != null)
            {
                Data.Read();
            }
        }
        
        public override void Exit(bool saveData = true)
        {
            if (saveData)
            {
                if (Data != null)
                {
                    Data.Write();
                }
            }
        }
    }
}