using System;
using UnityEngine;

namespace YG_EventSystem {
    public static class GameObjectEvents {
        public static T OnEvent<T> (this GameObject gameObject, Action action) where T : MonoBehaviour, IGameObjectEvent {
            T trigger = gameObject.GetComponent<T> ();
            if (!trigger)
                trigger = gameObject.AddComponent<T> ();
            trigger.action += action;
            trigger.Active ();
            return trigger;
        }
        public static T OnEvent<T, P> (this GameObject gameObject, Action<P> action) where T : MonoBehaviour, IGameObjectEvent<P> {
            T trigger = gameObject.GetComponent<T> ();
            if (!trigger)
                trigger = gameObject.AddComponent<T> ();
            trigger.action += action;
            trigger.Active ();
            return trigger;

        }
        public static T OnEvent<T, P1, P2> (this GameObject gameObject, Action<P1, P2> action) where T : MonoBehaviour, IGameObjectEvent<P1, P2> {
            T trigger = gameObject.GetComponent<T> ();
            if (!trigger)
                trigger = gameObject.AddComponent<T> ();
            trigger.action += action;
            trigger.Active ();
            return trigger;
        }
        public static T OnEvent<T, P1, P2, P3> (this GameObject gameObject, Action<P1, P2, P3> action) where T : MonoBehaviour, IGameObjectEvent<P1, P2, P3> {
            T trigger = gameObject.GetComponent<T> ();
            if (!trigger)
                trigger = gameObject.AddComponent<T> ();
            trigger.action += action;
            trigger.Active ();
            return trigger;
        }
    }
}