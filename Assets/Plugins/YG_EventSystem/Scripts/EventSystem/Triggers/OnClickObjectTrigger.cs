using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YG_EventSystem
{
    public class OnClickObjectTrigger : YGES_Standart, IGameObjectEvent<GameObject, PointerEventData>, IPointerClickHandler
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

        public void OnPointerClick(PointerEventData eventData)
        {
            action?.Invoke(gameObject, eventData);
        }
    }
}
