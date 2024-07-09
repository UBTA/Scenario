using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.Animator;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


namespace EblanDev.ScenarioCore.UIFramework.UIElements
{
    public class View : SerializedMonoBehaviour
    {
        /// <summary>
        /// Ссылка на CanvasGroup этой View.
        /// </summary>
        public CanvasGroup CG;
        /// <summary>
        /// Ссылка на RectTransform этой View.
        /// </summary>
        public RectTransform Rect;
        
        /// <summary>
        /// Задает анимацию Show.
        /// </summary>
        [TypeFilter("FilterAnimations")] [SerializeField] protected IViewAnimator InAnimation;
        /// <summary>
        /// Задает анимацию Hide.
        /// </summary>
        [TypeFilter("FilterAnimations")] [SerializeField] protected IViewAnimator OutAnimation;

        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] public Vector3 InitLocalPosition;
        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] public Quaternion InitLocalRotation;
        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] public Vector3 InitLocalScale;
        
        /// <summary>
        /// Твин контролирующий Show и Hide анимации.
        /// </summary>
        protected Tween animationTween;

        /// <summary>
        /// Инициализация View.
        /// Подписка на внутренние события, стартовых кеширование параметров View.
        /// </summary>
        public virtual void Init()
        {
            InitLocalPosition = transform.localPosition;
            InitLocalRotation = transform.localRotation;
            InitLocalScale = transform.localScale;

            if (!gameObject.activeSelf)
            {
                HideImmediately();
            }
        }

        /// <summary>
        /// Показать View анимированно.
        /// </summary>
        /// <returns>
        /// Возвращает ссылку на твин управляющий вызывавемой анимацией.
        /// </returns>
        public Tween Show()
        {
            animationTween?.Kill();
            if (InAnimation != null)
            {
                OnShowStart(false);
                gameObject.SetActive(true);
                animationTween = InAnimation.Animate(this)
                    .OnComplete(() =>
                    {
                        OnShowEnd(false);
                    });
            }
            else
            {
                OnShowStart(false);
                gameObject.SetActive(true);
                OnShowEnd(false);
            }

            return animationTween;
        }

        /// <summary>
        /// Показать View мгновенно.
        /// </summary>
        public void ShowImmediately()
        {
            animationTween?.Kill();
            
            OnShowStart(true);

            if (InAnimation != null)
            {
                gameObject.SetActive(true);
                InAnimation.AnimateImmediately(this);
            }
            else
            {
                gameObject.SetActive(true);
            }
            
            OnShowStart(false);
        }

        /// <summary>
        /// Скрыть View анимированно.
        /// </summary>
        /// <returns>
        /// Возвращает ссылку на твин управляющий вызывавемой анимацией.
        /// </returns>
        public Tween Hide()
        {
            animationTween?.Kill(true);

            if (OutAnimation != null)
            {
                OnHideStart(false);
                animationTween = OutAnimation.Animate(this)
                    .OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                        OnHideEnd(false);
                    });
            }
            else
            {
                OnHideStart(false);
                gameObject.SetActive(false);
                OnHideEnd(false);
            }

            return animationTween;
        }

        /// <summary>
        /// Скрыть View мгновенно.
        /// </summary>
        public void HideImmediately()
        {
            animationTween?.Kill();
            
            OnHideStart(true);

            if (OutAnimation != null)
            {
                OutAnimation.AnimateImmediately(this);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }

            OnHideEnd(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="immediately"></param>
        protected virtual void OnShowStart(bool immediately)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="immediately"></param>
        protected virtual void OnShowEnd(bool immediately)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="immediately"></param>
        protected virtual void OnHideStart(bool immediately)
        {
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="immediately"></param>
        protected virtual void OnHideEnd(bool immediately)
        {
        }

        /// <summary>
        /// Ивент монобеха для очистки асинхронщины.
        /// При оверрайде обязательно вызвать базу.
        /// </summary>
        protected virtual void OnDestroy()
        {
            animationTween.Kill(true);
            transform.DOKill(true);
        }
        
        /// <summary>
        /// Фильтр типов анимаций для отображения в инспекторе.
        /// </summary>
        /// <returns>
        /// Возвращает коллекцию типов.
        /// </returns>
        protected virtual IEnumerable<Type> FilterAnimations()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> result = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (typeof(IViewAnimator).IsAssignableFrom(type))
                    {
                        result.Add(type);
                    }
                }
            }

            return result;
        }
    }
}