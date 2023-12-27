using UnityEngine;

namespace EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable
{
    public class Hit
    {
        public PhysActivator Activator;
        public Vector3 Point;
        public Vector3 Direction;

        public Hit()
        {
        }
        public Hit(PhysActivator activator, Vector3 point, Vector3 direction)
        {
            Activator = activator;
            Point = point;
            Direction = direction;
        }
    }
}