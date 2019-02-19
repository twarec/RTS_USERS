using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG_EventSystem
{
    public class OnDestroyTrigger : MonoBehaviour, IGameObjectEvent<GameObject>
    {
        public Action<GameObject> action { get; set; }

        public void Active()
        {
            throw new NotImplementedException();
        }
        private void OnDestroy()
        {
            action?.Invoke(gameObject);
        }
    }
}