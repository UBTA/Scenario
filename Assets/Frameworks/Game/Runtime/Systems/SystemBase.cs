using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{
    public abstract class SystemBase : MonoBehaviour, ISystem
    {
        public virtual bool TryGetData(out IData data)
        {
            data = null;
            return false;
        }

        public abstract void Prepare();

        public abstract void Init();

        public abstract void Exit(bool saveData);
    }
}