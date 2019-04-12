using System;
using UnityEngine;
using System.Diagnostics;

namespace YG_EventSystem {
    [AddComponentMenu ("YG_EventSystem/GameEvent")]
    public class GameEvent : MonoBehaviour {
        private static GameEvent obj;
        private static GameEvent Obj {
            get {
                if (!obj) {
                    obj = new GameObject ("Game events").AddComponent<GameEvent> ();
                    DontDestroyOnLoad (obj);
                }
                return obj;
            }
        }

        public Action<float> updateEvent;
        public Action<float> fixedUpdateEvent;
        public Action<float> lateUpdateEvent;

        public float UpdateTime = 0;
        public float FixedUpdateTime = 0;
        public float LateUpdateTime = 0;

        private float t;
        private void Update () {
#if UNITY_EDITOR
            Stopwatch sw = new Stopwatch();
            sw.Start();
            t = Time.deltaTime;
            updateEvent?.Invoke(t);
            sw.Stop();
            UpdateTime = sw.ElapsedTicks;
#else
            t = Time.deltaTime;
            updateEvent?.Invoke (t);
#endif
        }
        private void FixedUpdate () {
#if UNITY_EDITOR
            Stopwatch sw = new Stopwatch();
            sw.Start();
            t = Time.deltaTime;
            fixedUpdateEvent?.Invoke(t);
            sw.Stop();
            FixedUpdateTime = sw.ElapsedTicks;
#else
            fixedUpdateEvent?.Invoke (t);
#endif
        }
        private void LateUpdate () {
#if UNITY_EDITOR
            Stopwatch sw = new Stopwatch();
            sw.Start();
            t = Time.deltaTime;
            lateUpdateEvent?.Invoke(t);
            sw.Stop();
            LateUpdateTime = sw.ElapsedTicks;
#else
            lateUpdateEvent?.Invoke (t);
#endif

        }
        public static void AddEvent (Action<float> action, Method method) {
            switch (method) {
                case Method.Update:
                    Obj.updateEvent += action;
                    break;
                case Method.LateUpdate:
                    Obj.lateUpdateEvent += action;
                    break;
                case Method.FixedUpdate:
                    Obj.fixedUpdateEvent += action;
                    break;
            }
        }
        public static void AddEvent (EventData eventData)
        {
            AddEvent(eventData.action, eventData.method);
        }
        public static void AddEvent(params EventData[] eventData)
        {
            foreach (var v in eventData)
                AddEvent(v);
        }
        public static void RemoveEvent (Action<float> action, Method method) {
            if (obj) {
                switch (method) {
                    case Method.Update:
                        Obj.updateEvent -= action;
                        break;
                    case Method.LateUpdate:
                        Obj.lateUpdateEvent -= action;
                        break;
                    case Method.FixedUpdate:
                        Obj.fixedUpdateEvent -= action;
                        break;
                }
            }
        }
        public static void RemoveEvent(EventData eventData)
        {
            RemoveEvent(eventData.action, eventData.method);
        }
        public static void RemoveEvent(params EventData[] eventData)
        {
            foreach (var v in eventData)
                RemoveEvent(v);
        }

        internal static void AddEvent(Action lateUpdate1, Method lateUpdate2)
        {
            throw new NotImplementedException();
        }
    }
}