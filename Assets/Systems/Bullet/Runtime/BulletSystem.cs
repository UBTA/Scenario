using EblanDev.ScenarioCore.GameFramework.Systems;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.BulletSystemUnit
{
    public class BulletSystem : StaticSystemDated<BulletSystemData>
    {
        private static BulletWorker bullets;
        
        public override void Init()
        {
            bullets = new BulletWorker(transform);
        }
        
        public static BulletInstance Shoot(BulletID ID, Vector3 from, Vector3 direction)
        {
            var bullet = bullets.Create(DataStatic.Bullet(ID));
            bullet.Impulse(from, direction);

            return bullet;
        }
    }
}