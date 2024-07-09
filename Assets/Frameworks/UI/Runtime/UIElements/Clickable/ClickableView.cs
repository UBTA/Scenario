using System;
using System.Collections.Generic;
using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.Animator;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;


namespace EblanDev.ScenarioCore.UIFramework.UIElements
{
    /// <summary>
    /// Базовый view обработчик нажатий.
    /// </summary>
    public class ClickableView : View, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        /// <summary>
        /// Анимация при зажатии.
        /// </summary>
        [TypeFilter("FilterAnimations")] [SerializeField] protected IViewAnimator PointerDown;
        /// <summary>
        /// Анимация при отпускании.
        /// </summary>
        [TypeFilter("FilterAnimations")] [SerializeField] protected IViewAnimator PointerUp;

        /// <summary>
        /// Твиин управляющий анимациями нажатий.
        /// </summary>
        protected Tween pointerTween;

        /// <summary>
        /// Событие на зажатие.
        /// </summary>
        public event Action OnDownEvent = delegate { };
        /// <summary>
        /// Событие на отпускание.
        /// </summary>
        public event Action OnUpEvent = delegate { };
        /// <summary>
        /// Событие на клик.
        /// </summary>
        public event Action OnClickEvent = delegate { };
        
        /// <summary>
        /// Обработчик зажатия.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDownEvent?.Invoke();
            
            pointerTween?.Kill(true);
            if (PointerDown != null)
            {
                pointerTween = PointerDown.Animate(this);
            }
        }

        /// <summary>
        /// Обработчик отпускания.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            OnUpEvent?.Invoke();
            
            pointerTween?.Kill(true);
            if (PointerUp != null)
            {
                pointerTween = PointerUp.Animate(this);
            }
        }

        /// <summary>
        /// Обработчик клика.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
        }
    }
}