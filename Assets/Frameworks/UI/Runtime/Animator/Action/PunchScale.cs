using System;
using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.UIElements;
using UnityEngine;


namespace EblanDev.ScenarioCore.UIFramework.Animator.Action
{
    /// <summary>
    /// Анимирует панч скейла на устоновленный оффсет(punch) заданной View по параметрам.
    /// </summary>
    [Serializable]
    public sealed class PunchScale : ViewAnimatorBase
    {
        [SerializeField] private Vector3 punch;
        [SerializeField] private float delay;
        [SerializeField] private float duration;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float elasticity = 1f;
        [SerializeField] private Ease ease;
        
        /// <summary>
        /// Выполняет анимацию по заданным параметрам.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        public override Tween Animate(View view)
        {
            if (view == null) return DOTween.Sequence();
            
            return view.transform.DOPunchScale(punch, duration, vibrato, elasticity)
                .SetDelay(delay)
                .SetEase(ease)
                .SetUpdate(true);
        }

        /// <summary>
        /// Выполняет анимацию по заданным параметрам мгновенно.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        public override void AnimateImmediately(View view)
        {
            view.transform.localScale = view.InitLocalScale;
        }
    }
}