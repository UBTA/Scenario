using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.UIElements;


namespace EblanDev.ScenarioCore.UIFramework.Animator
{
    /// <summary>
    /// Базовая абстрактная реализация аниматора.
    /// </summary>
    public abstract class ViewAnimatorBase : IViewAnimator
    {
        /// <summary>
        /// Выполняет анимацию по заданным параметрам.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        public abstract Tween Animate(View view);

        /// <summary>
        /// Выполняет анимацию по заданным параметрам мгновенно.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        public abstract void AnimateImmediately(View view);
    }
}