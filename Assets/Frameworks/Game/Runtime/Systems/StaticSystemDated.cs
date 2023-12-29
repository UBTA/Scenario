using EblanDev.ScenarioCore.GameFramework.Data;

namespace EblanDev.ScenarioCore.GameFramework.Systems
{ 
    public class StaticSystemDated<D> : SystemDated<D> where D : IData
    {
        protected static D DataStatic;

        public override void Prepare()
        {
            base.Prepare();
            DataStatic = Data;
        }

        public override void Init()
        {
        }
    }
}