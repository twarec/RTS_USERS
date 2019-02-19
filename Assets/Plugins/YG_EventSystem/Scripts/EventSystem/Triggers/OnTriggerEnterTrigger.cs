using System;
using UnityEngine;

namespace YG_EventSystem {
    public class OnTriggerEnterTrigger : MonoBehaviour, IGameObjectEvent<Collider> {
        public Action<Collider> action { get; set; }

        public void Active () { }

        private void OnTriggerEnter (Collider other) {
            action.Invoke (other);
        }
    }
}