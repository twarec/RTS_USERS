using System;
using UnityEngine;

namespace YG_EventSystem
{
    public class TriggerShablon : YGES_Standart, IGameObjectEvent<GameObject>
    {
        /// <summary>
        /// Событие для триггера
        /// </summary>
        public Action<GameObject> action { get; set; }

        /// <summary>
        /// Вызывается при инициализации триггерра
        /// </summary>
        public void Active()
        {
            
        }
    }
}
