using System;
using UnityEngine;

namespace YG_EventSystem {
    public class OnTriggerOverTrigger : MonoBehaviour, IGameObjectEvent<Collider> {
        public Action<Collider> action { get; set; }

        public void Active () { }

        private void OnTriggerStay (Collider other) {
            action (other);
        }
    }
}