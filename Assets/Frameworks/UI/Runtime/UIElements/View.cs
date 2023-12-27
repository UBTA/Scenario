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
        /// Задает Attention анимацию.
        /// Зацикленная анимация повторяющаяся указанное количество раз.
        /// </summary>
        [TypeFilter("FilterAnimations")] [SerializeField] protected IViewAnimator Attention;

        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] public Action<bool> OnShowStartEvent = delegate { };
        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] public Action<bool> OnShowEndEvent = delegate { };
        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] public Action<bool> OnHideStartEvent = delegate { };
        /// <summary>
        /// 
        /// </summary>
        [NonSerialized] public Action<bool> OnHideEndEvent = delegate { };

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
        /// Твин контролирующий Attention анимацию.
        /// </summary>
        protected Tween attentionTween;

        /// <summary>
        /// 
        /// </summary>
        protected int attentionCount = 0;

        private CancellationTokenSource _attentionCancellation;
        
        /// <summary>
        /// Инициализация View.
        /// Подписка на внутренние события, стартовых кеширование параметров View.
        /// </summary>
        public void Init()
        {
            OnShowStartEvent += OnShowStart;
            OnShowEndEvent += OnShowEnd;
            OnHideStartEvent += OnHideStart;
            OnHideEndEvent += OnHideEnd;

            InitLocalPosition = transform.localPosition;
            InitLocalRotation = transform.localRotation;
            InitLocalScale = transform.localScale;

            OnInit();

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
                gameObject.SetActive(true);

                OnShowStartEvent?.Invoke(false);
                
                animationTween = InAnimation.Animate(this)
                    .OnComplete(() =>
                    {
                        OnShowEndEvent?.Invoke(false);
                    });
            }
            else
            {
                OnShowStartEvent?.Invoke(false);

                gameObject.SetActive(true);
                
                OnShowEndEvent?.Invoke(false);
            }

            return animationTween;
        }

        /// <summary>
        /// Показать View мгновенно.
        /// </summary>
        public void ShowImmediately()
        {
            animationTween?.Kill();
            
            OnShowStartEvent?.Invoke(true);

            if (InAnimation != null)
            {
                gameObject.SetActive(true);
                InAnimation.AnimateImmediately(this);
            }
            else
            {
                gameObject.SetActive(true);
            }
            
            OnShowEndEvent?.Invoke(true);
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
            attentionTween?.Kill(true);
            
            if (OutAnimation != null)
            {
                OnHideStartEvent?.Invoke(false);
                
                animationTween = OutAnimation.Animate(this)
                    .OnComplete(() =>
                    {
                        OnHideEndEvent?.Invoke(false);
                        gameObject.SetActive(false);
                    });
            }
            else
            {
                OnHideStartEvent?.Invoke(false);
                
                gameObject.SetActive(false);

                OnHideEndEvent?.Invoke(false);
            }

            return animationTween;
        }

        /// <summary>
        /// Скрыть View мгновенно.
        /// </summary>
        public void HideImmediately()
        {
            animationTween?.Kill();
            
            OnHideStartEvent?.Invoke(true);

            if (OutAnimation != null)
            {
                OutAnimation.AnimateImmediately(this);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }

            OnHideEndEvent?.Invoke(true);
        }
        
        /// <summary>
        /// Включить Attention анимацию.
        /// </summary>
        /// <param name="count">
        /// Количество повторений
        /// </param>
        public void EnableAttention(int count)
        {
            if (Attention == null)
            {
                return;
            }

            if (count == -1 && attentionCount < 0)
            {
                return;
            }

            if (attentionCount == 0)
            {
                _attentionCancellation?.Cancel();
                _attentionCancellation = CancellationTokenSource
                    .CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
                
                AttentionLoop(_attentionCancellation.Token).Forget();
            }

            attentionCount = count;
        }
        
        /// <summary>
        /// Отключить Attention анимацию.
        /// </summary>
        public virtual void DisableAttention()
        {
            _attentionCancellation?.Cancel();
            attentionCount = 0;
            attentionTween.Kill();
        }
        
        private async UniTask AttentionLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested && attentionCount != 0)
            {
                if (Attention == null)
                {
                    return;
                }

                attentionCount--;
                attentionTween.Kill(true);
                attentionTween = Attention.Animate(this);

                if (!attentionTween.active) 
                {
                    return;
                }

                while (attentionTween.active && !attentionTween.IsComplete())
                {
                    await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnInit()
        {
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
            _attentionCancellation?.Cancel();
            attentionCount = 0;
            attentionTween.Kill();
            
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