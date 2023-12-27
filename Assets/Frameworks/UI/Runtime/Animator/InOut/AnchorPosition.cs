using System;
using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.UIElements;
using UnityEngine;


namespace EblanDev.ScenarioCore.UIFramework.Animator.InOut
{
    /// <summary>
    /// Анимирует перемещение относительно якорной позиции заданной View по параметрам.
    /// </summary>
    [Serializable]
    public sealed class AnchorPosition : ViewAnimatorBase
    {
        [SerializeField] private float delay;
        [SerializeField] private float duration;
        [SerializeField] private Vector2 anchorPosition;
        [SerializeField] private Ease ease;
        
        /// <summary>
        /// Выполняет анимацию по заданным параметрам.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        public override Tween Animate(View view)
        {
            var tween = view.Rect.DOAnchorPos(anchorPosition, duration)
                .SetEase(ease)
                .SetDelay(delay)
                .SetUpdate(true);
            return tween;
        }

        /// <summary>
        /// Выполняет анимацию по заданным параметрам мгновенно.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        public override void AnimateImmediately(View view)
        {
            view.Rect.anchoredPosition = anchorPosition;
        }
    }
}