using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
    public class DraggerInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<UIInput> OnEventBeginDrag;
        public event Action<UIInput> OnEventDrag;
        public event Action<UIInput> OnEventEndDrag;

        public bool IsDragging = false;
        
        private Vector2 start;
        private Vector2 lastPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            IsDragging = true;
            start = eventData.position;
            lastPosition = eventData.position;

            OnEventBeginDrag?.Invoke(
                new UIInput(
                    eventData.pointerId, 
                    eventData.position, 
                    Vector2.zero, 
                    Vector2.zero,
                    Camera.main.ScreenPointToRay(start)));
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnEventDrag?.Invoke(
                new UIInput(
                    eventData.pointerId,
                    eventData.position,
                    eventData.position - start,
                    eventData.position - lastPosition,
                    Camera.main.ScreenPointToRay(eventData.position))
            );
            lastPosition = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEventEndDrag?.Invoke(
                new UIInput(
                    eventData.pointerId,
                    eventData.position,
                    eventData.position - start,
                    eventData.position - lastPosition,
                    Camera.main.ScreenPointToRay(eventData.position))
            );
            
            lastPosition = Vector2.zero;
            start = Vector2.zero;
            IsDragging = false;
        }
    }
}