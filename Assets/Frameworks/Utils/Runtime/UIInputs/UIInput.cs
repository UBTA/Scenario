using UnityEngine;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
    public struct UIInput
    {
        public int TouchID;
        public Vector2 Position;
        public Vector2 Offset;
        public Vector2 DeltaDrag;
        public Ray WorldRay;

        public UIInput(int touchID, Vector2 position, Vector2 offset, Vector2 deltaDrag, Ray worldRay)
        {
            TouchID = touchID;
            Position = position;
            Offset = offset;
            DeltaDrag = deltaDrag;
            WorldRay = worldRay;
        }
    }
}
