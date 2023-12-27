using System;
using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.UIElements;
using UnityEngine;

namespace EblanDev.ScenarioCore.UIFramework.Animator.InOut
{
    /// <summary>
    /// Анимирует скейл заданной View по параметрам.
    /// </summary>
    [Serializable]
    public sealed class Scale : ViewAnimatorBase
    {
       [SerializeField] private float delay;
       [SerializeField] private float duration;
       [SerializeField] private Vector3 scale;
       [SerializeField] private Ease ease;


       /// <summary>
       /// Выполняет анимацию по заданным параметрам.
       /// </summary>
       /// <param name="view">
       /// View к которой применяется анимация.
       /// </param>
       public override Tween Animate(View view)
       {
           var tween = view.transform.DOScale(scale, duration)
               .SetDelay(delay)
               .SetEase(ease)
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
           view.transform.localScale = scale;
       }
    }
}