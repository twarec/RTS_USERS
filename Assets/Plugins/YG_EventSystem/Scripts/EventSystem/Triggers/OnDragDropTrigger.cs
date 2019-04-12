using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YG_EventSystem
{
    public class OnDragDropTrigger : YGES_Standart, IGameObjectEvent<GameObject, PointerEventData>, IDropHandler, IDragHandler, IPointerDownHandler
    {
        /// <summary>
        /// Событие для триггера
        /// </summary>
        public Action<GameObject, PointerEventData> action { get; set; }

        /// <summary>
        /// Вызывается при инициализации триггерра
        /// </summary>
        public void Active()
        {
            
        }


        private RectTransform rt;
        private Vector2 startPos;
        public override void EOnEnable()
        {
            rt = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            rt.anchoredPosition = eventData.position + startPos;
        }

        public void OnDrop(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            startPos =  rt.anchoredPosition - eventData.position;
        }
    }
}
