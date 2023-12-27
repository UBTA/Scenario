using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
    public class ClickInput : MonoBehaviour, IPointerClickHandler
    {
        public event Action<UIInput> OnEventClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnEventClick?.Invoke(
                new UIInput(
                    eventData.pointerId,
                    eventData.position,
                    eventData.position,
                    Vector2.zero, 
                    Camera.main.ScreenPointToRay(eventData.position)));
        }
    }
}