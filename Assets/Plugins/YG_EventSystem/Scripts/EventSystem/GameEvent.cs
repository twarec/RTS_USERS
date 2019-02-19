using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;

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
        private Dictionary<int, TaskStruct> TaskDictionary = new Dictionary<int, TaskStruct> ();
        private Task task;

        private float t;
        private bool TaskStop = false;
        private void Awake () {
            //task = Task.Run (TaskWhile);
        }
        private void Update () {
            Profiler.BeginSample ("Game event UPDATE");
            t = Time.deltaTime;
            updateEvent?.Invoke (t);
            Profiler.EndSample ();
        }
        private void TaskWhile () {
            TaskStruct ts;
            while (true) {
                if (TaskStop)
                    break;
                int count = TaskDictionary.Count;
                if (count != 0) {
                    for (int i = 0; i < count; i++) {
                        ts = TaskDictionary.ElementAt (i).Value;
                        if (!ts.Dicrimet ()) {
                            TaskDictionary.Remove (TaskDictionary.ElementAt (i).Key);
                            count--;
                            i--;
                            if (ts.IsMain) {
                                Action<float> updateAction = null;
                                updateAction = t => {
                                    ts.GetUpdateAction?.Invoke (t);
                                    RemoveEvent (updateAction, Method.Update);
                                };
                                AddEvent (updateAction, Method.Update);
                            }
                        }
                    }
                    TaskDictionary.AsParallel ().ForAll (v => v.Value.Action?.Invoke ());
                }
            }
        }
        public static void AddTaskEvent (int key, Action action, int loop, Action<float> updateAction) {
            Obj.TaskDictionary.Add (key, new TaskStruct (action, loop, updateAction));
        }
        private void FixedUpdate () {
            Profiler.BeginSample ("Game event FixedUpdate");
            fixedUpdateEvent?.Invoke (t);
            Profiler.EndSample ();
        }
        private void LateUpdate () {
            Profiler.BeginSample ("Game event LateUpdate");
            lateUpdateEvent?.Invoke (t);
            Profiler.EndSample ();
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
        private void OnDestroy () {
            TaskStop = true;
        }
    }
}