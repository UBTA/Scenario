using System.Collections.Generic;
using EblanDev.ScenarioCore.Systems.FXSystemUnit;
using UnityEngine;

namespace EblanDev.ScenarioCore.FXDecals
{
    [CreateAssetMenu(fileName = "FXDataExtended", menuName = "ScenarioFramework/Systems/FXDataExtended", order = 1)]
    public class FXDataExtended : FXData
    {
        [SerializeField] private List<DecalInstance> decals;
        
        public DecalInstance GetDecal(DecalID ID)
        {
            foreach (var decal in decals)
            {
                if (decal.ID == ID)
                {
                    return decal;
                }
            }

            if (decals.Count != 0)
            {
                return decals[0];
            }

            return null;
        }
    }
}