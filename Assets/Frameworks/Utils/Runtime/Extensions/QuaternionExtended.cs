using System;
using UnityEngine;
using UnityEngine.Animations;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
    public static class QuaternionExtensions
    {
        public static Quaternion CutRotation(this Quaternion q, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return XRotation(q);
                case Axis.Y:
                    return YRotation(q);
                case Axis.Z:
                    return ZRotation(q);
                default:
                    throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }
        
        public static Quaternion XRotation(this Quaternion q)
        {
            float theta = Mathf.Atan2(q.x, q.w);
            return new Quaternion(Mathf.Sin(theta), 0, 0, Mathf.Cos(theta));
        }
        
        public static Quaternion YRotation(this Quaternion q)
        {
            float theta = Mathf.Atan2(q.y, q.w);
            return new Quaternion(0, Mathf.Sin(theta), 0, Mathf.Cos(theta));
        }
        
        public static Quaternion ZRotation(this Quaternion q)
        {
            float theta = Mathf.Atan2(q.z, q.w);
            return new Quaternion(0, 0 , Mathf.Sin(theta), Mathf.Cos(theta));
        }
    }
}