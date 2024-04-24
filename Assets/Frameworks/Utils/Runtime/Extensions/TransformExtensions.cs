using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
    public static class TransformExtensions
    {
        public static async UniTask Move(this Transform movable, Transform from, Transform to, float time, bool isLocal = false,
            Ease easing = Ease.Linear)
        {
           await movable.MoveAsync(from, to, time, isLocal, easing);
        }
        
        public static async UniTask MoveAsync(this Transform movable, Transform from, Transform to, float time, bool isLocal  = false,
            Ease easing = Ease.Linear)
        {
            var lerpCoef = 0f;
            var startTime = Time.time;

            while (lerpCoef < 1f)
            {
                await UniTask.Yield();

                if (ArePointsNull(movable, from, to))
                {
                    return;
                }
                
                var fromPosition = from.position;
                var fromRotation = from.rotation;
            
                var toPosition = to.position;
                var toRotation = to.rotation;

                var currentTime = Time.time - startTime;
                lerpCoef = currentTime / time;

                var easedCoef = DOVirtual.EasedValue(0f, 1f, lerpCoef, easing);
                if (isLocal)
                {
                    movable.localPosition = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                    movable.localRotation = Quaternion.Lerp(fromRotation, toRotation, easedCoef);
                }
                else
                {
                    movable.position = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                    movable.rotation = Quaternion.Lerp(fromRotation, toRotation, easedCoef);
                }
            }
        }
        
        public static async UniTask MoveAsync(this Transform movable, Vector3? from, Vector3? to, float time, bool isLocal  = false,
            Ease easing = Ease.Linear)
        {
            var lerpCoef = 0f;
            var startTime = Time.time;

            while (lerpCoef < 1f)
            {
                await UniTask.Yield();

                if (ArePointsNull(movable, from, to))
                {
                    return;
                }
                
                var fromPosition = from.Value;
                var toPosition = to.Value;

                var currentTime = Time.time - startTime;
                lerpCoef = currentTime / time;

                var easedCoef = DOVirtual.EasedValue(0f, 1f, lerpCoef, easing);
                if (isLocal)
                {
                    movable.localPosition = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                }
                else
                {
                    movable.position = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                }
            }
        }
        
        public static async UniTask MoveAsync(this Transform movable, Vector3 from, Vector3 to, float time, bool isLocal  = false,
            Ease easing = Ease.Linear)
        {
            var lerpCoef = 0f;
            var startTime = Time.time;

            while (lerpCoef < 1f)
            {
                await UniTask.Yield();

                if (movable == null)
                {
                    return;
                }
                
                var fromPosition = from;
                var toPosition = to;

                var currentTime = Time.time - startTime;
                lerpCoef = currentTime / time;

                var easedCoef = DOVirtual.EasedValue(0f, 1f, lerpCoef, easing);
                if (isLocal)
                {
                    movable.localPosition = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                }
                else
                {
                    movable.position = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                }
            }
        }
        
        public static async UniTask MoveToAsync(this Transform movable, Transform to, float time, bool isLocal  = false,
            Ease easing = Ease.Linear)
        {
            var lerpCoef = 0f;
            var startTime = Time.time;

            while (lerpCoef < 1f)
            {
                await UniTask.Yield();
                
                if (movable == null)
                {
                    return;
                }
                
                if (to == null)
                {
                    return;
                }

                var fromPosition = movable.position;
                var fromRotation = movable.rotation;
            
                var toPosition = to.position;
                var toRotation = to.rotation;

                var currentTime = Time.time - startTime;
                lerpCoef = currentTime / time;

                var easedCoef = DOVirtual.EasedValue(0f, 1f, lerpCoef, easing);
                if (isLocal)
                {
                    movable.localPosition = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                    movable.localRotation = Quaternion.Lerp(fromRotation, toRotation, easedCoef);
                }
                else
                {
                    movable.position = Vector3.Lerp(fromPosition, toPosition, easedCoef);
                    movable.rotation = Quaternion.Lerp(fromRotation, toRotation, easedCoef);
                }
            }
        }

        private static bool ArePointsNull(Transform movable, Transform from, Transform to)
        {
            if (movable == null)
            {
                Debug.LogWarning("Movable not set to move asynchronously");
                return true;
            }

            if (from == null)
            {
                Debug.LogWarning("Movable start point not set to move asynchronously");
                return true;
            }

            if (to == null)
            {
                Debug.LogWarning("Movable target point not set to move asynchronously");
                return true;
            }

            return false;
        }
        
        private static bool ArePointsNull(Transform movable, Vector3? from, Vector3? to)
        {
            if (movable == null)
            {
                Debug.LogWarning("Movable not set to move asynchronously");
                return true;
            }

            if (from.HasValue == false)
            {
                Debug.LogWarning("Movable start point not set to move asynchronously");
                return true;
            }

            if (to.HasValue == false)
            {
                Debug.LogWarning("Movable target point not set to move asynchronously");
                return true;
            }

            return false;
        }
    }
}