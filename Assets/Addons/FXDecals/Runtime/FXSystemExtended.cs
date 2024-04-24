using EblanDev.ScenarioCore.Systems.FXSystemUnit;
using UnityEngine;

namespace EblanDev.ScenarioCore.FXDecals
{
    public class FXSystemExtended : FXSystem
    {
        private static DecalWorker decals;

        public override void Init()
        {
            base.Init();
            decals = new DecalWorker(transform);
        }
        
        public static void Decal(int id, Vector3 pos,  Vector3 dir, Transform parent = null)
        {
            var data = DataStatic as FXDataExtended;
            if (data != null)
            {
                var decal = decals.Create(data.GetDecal(id));

                decal.transform.position = pos;

                if (parent != null)
                {
                    decal.ParentTo(parent, thisT);
                }

                decal.Place(dir);
            }
            else
            {
                Debug.Log("FX addon should use FXDataExtended");
            }
        } 
    }
}