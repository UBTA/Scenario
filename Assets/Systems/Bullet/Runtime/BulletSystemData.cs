using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.BulletSystemUnit
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "ScenarioFramework/Systems/BulletData", order = 1)]
    public class BulletSystemData : DataClear
    {
        [SerializeField] private BulletInstance[] bullets;

        public BulletInstance Bullet(BulletID BulletID)
        {
            foreach (var bullet in bullets)
            {
                if (bullet.ID == BulletID)
                {
                    return bullet;
                }
            }
            
            return bullets[0];
        }
    }
}