using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.UIElements;

namespace EblanDev.ScenarioCore.UIFramework.Animator
{
    /// <summary>
    /// Интерфейс через который с аниматором работают View.
    /// </summary>
    public interface IViewAnimator
    {
        /// <summary>
        /// Выполняет анимацию по заданным параметрам.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        Tween Animate(View view);
        
        /// <summary>
        /// Выполняет анимацию по заданным параметрам мгновенно.
        /// </summary>
        /// <param name="view">
        /// View к которой применяется анимация.
        /// </param>
        void AnimateImmediately(View view);
    }
}