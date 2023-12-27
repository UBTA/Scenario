using EblanDev.ScenarioCore.UIFramework.Bus;
using EblanDev.ScenarioCore.UIFramework.UIElements;
using UnityEngine;

namespace EblanDev.ScenarioCore.UIFramework
{
    /// <summary>
    /// Хэндлит работу экранов и их процесс инициализации.
    /// </summary>
    /// <typeparam name="B">
    /// Тип шины (необходимо унаследовааться от UIBus).
    /// </typeparam>
    public class UI<B> : MonoBehaviour where B : UIBus
    {
        [SerializeField] private ScreenView<B>[] screens;

        /// <summary>
        /// Инициализация всего интерфейса.
        /// </summary>
        /// <param name="Bus"></param>
        public void Init(B Bus)
        {
            var length = screens.Length;
            for (var i = 0; i < length; i++)
            {
                var window = screens[i];
                window.SetBus(Bus);
                window.Init();

                window.HideImmediately();

                if (window.ShowOnInit)
                {
                    window.ShowImmediately();
                }
            }
        }

        /// <summary>
        /// Получение экрана по типу.
        /// </summary>
        /// <typeparam name="T">
        /// Тип экрана.
        /// </typeparam>
        /// <returns>
        /// Возвращает ссылку на экран.
        /// </returns>
        public T Get<T>() where T : ScreenView<B>
        {
            var w = default(ScreenView<B>);
            var length = screens.Length;
            for (var i = 0; i < length; i++)
            {
                var screen = screens[i];

                if (w == null && screen is T)
                {
                    w = screen;
                }
            }

            return w as T;
        }
        
        /// <summary>
        /// Показ экрана с его анимацией (если есть).
        /// </summary>
        /// <param name="isSolo">
        /// Если true все остальные экраны (без доп условий) будут скрыты.
        /// </param>
        /// <typeparam name="T">
        /// Возвращает ссылку на экран.
        /// </typeparam>
        public void Show<T>(bool isSolo = true) where T : ScreenView<B>
        {
            var w = default(ScreenView<B>);
            var length = screens.Length;
            for (var i = 0; i < length; i++)
            {
                var screen = screens[i];
                if (screen.Ignore) continue;

                if (w == null && screen is T)
                {
                    w = screen;
                    screen.Show();
                }
                else
                {
                    if (isSolo)
                    {
                        screen.Hide();
                    }
                }
            }
        }

        /// <summary>
        /// Показ экрана игнорируя анимацию (если есть).
        /// </summary>
        /// <param name="isSolo">
        /// Если true все остальные экраны (без доп условий) будут скрыты. 
        /// </param>
        /// <typeparam name="T">
        /// Возвращает ссылку на экран. 
        /// </typeparam>
        public void ShowImmediately<T>(bool isSolo = true) where T : ScreenView<B>
        {
            var w = default(ScreenView<B>);
            var length = screens.Length;
            for (var i = 0; i < length; i++)
            {
                var screen = screens[i];
                if (screen.Ignore) continue;

                if (w == null && screen is T)
                {
                    w = screen;
                    screen.ShowImmediately();
                }
                else
                {
                    if (isSolo)
                    {
                        screen.HideImmediately();
                    }
                }
            }
        }

        /// <summary>
        /// Сокрытие экрана с его анимацией (если есть).
        /// </summary>
        /// <typeparam name="T">
        /// Возвращает ссылку на экран. 
        /// </typeparam>
        public void Hide<T>() where T : ScreenView<B>
        {
            var w = default(ScreenView<B>);
            var length = screens.Length;
            for (var i = 0; i < length; i++)
            {
                var screen = screens[i];
                if (screen.Ignore) continue;
                if (w == null && screen is T)
                {
                    w = screen;
                    screen.Hide();
                }
            }
        }

        /// <summary>
        /// Сокрытие экрана экрана игнорируя анимацию (если есть).
        /// </summary>
        /// <typeparam name="T">
        /// Возвращает ссылку на экран. 
        /// </typeparam>
        public void HideImmediately<T>() where T : ScreenView<B>
        {
            var w = default(ScreenView<B>);
            var length = screens.Length;
            for (var i = 0; i < length; i++)
            {
                var screen = screens[i];
                if (screen.Ignore) continue;
                if (w == null && screen is T)
                {
                    w = screen;
                    screen.HideImmediately();
                }
            }
        }
    }
}