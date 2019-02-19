using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG_EventSystem {
    [AddComponentMenu ("YG_EventSystem/InputEvent")]
    public class InputEvent : MonoBehaviour {
        private static InputEvent obj;
        private static InputEvent Obj {
            get {
                if (!obj) {
                    obj = new GameObject ("Input events").AddComponent<InputEvent> ();
                    DontDestroyOnLoad (obj);
                }
                return obj;
            }
        }

        private Dictionary<string, Action<float>> inputs = new Dictionary<string, Action<float>> ();
        private Dictionary<int, Action> inputsMouseDown = new Dictionary<int, Action> ();
        private Dictionary<int, Action> inputsMouseUp = new Dictionary<int, Action> ();
        private Dictionary<int, Action> inputsMousePress = new Dictionary<int, Action> ();

        private void TOnUpdate (float obj) {
            foreach (var v in inputs) {
                float value = Input.GetAxis (v.Key);
                if (value != 0)
                    v.Value (value);
            }
        }
        private void TOnUpdate_MouseUp (float t) {
            foreach (var v in obj.inputsMouseUp)
                if (Input.GetMouseButtonUp (v.Key))
                    v.Value ();
        }
        private void TOnUpdate_MousePress (float t) {
            foreach (var v in obj.inputsMousePress)
                if (Input.GetMouseButton (v.Key))
                    v.Value ();
        }
        private void TOnUpdate_MouseDown (float t) {
            foreach (var v in obj.inputsMouseDown)
                if (Input.GetMouseButtonDown (v.Key))
                    v.Value ();
        }

        public static void AddInput (string name, Action<float> action) {
            if (Obj.inputs.ContainsKey (name))
                obj.inputs[name] += action;
            else {
                if (obj.inputs.Count == 0)
                    GameEvent.AddEvent (obj.TOnUpdate, Method.Update);
                obj.inputs.Add (name, action);
            }
        }
        public static void AddInput (int index, Action action, MouseType type) {
            switch (type) {
                case MouseType.Down:
                    if (Obj.inputsMouseDown.ContainsKey (index))
                        obj.inputsMouseDown[index] += action;
                    else {
                        if (obj.inputsMouseDown.Count == 0)
                            GameEvent.AddEvent (obj.TOnUpdate_MouseDown, Method.Update);
                        obj.inputsMouseDown.Add (index, action);
                    }
                    break;
                case MouseType.Press:
                    if (Obj.inputsMousePress.ContainsKey (index))
                        obj.inputsMousePress[index] += action;
                    else {
                        if (obj.inputsMousePress.Count == 0)
                            GameEvent.AddEvent (obj.TOnUpdate_MousePress, Method.Update);
                        obj.inputsMousePress.Add (index, action);
                    }
                    break;
                case MouseType.Up:
                    if (Obj.inputsMouseUp.ContainsKey (index))
                        obj.inputsMouseUp[index] += action;
                    else {
                        if (obj.inputsMouseUp.Count == 0)
                            GameEvent.AddEvent (obj.TOnUpdate_MouseUp, Method.Update);
                        obj.inputsMouseUp.Add (index, action);
                    }
                    break;
            }
        }
        public static void AddInput (InputData data) {
            AddInput (data.Name, data.Action);
        }
        public static void AddInput (params InputData[] datas) {
            foreach (var v in datas)
                AddInput (v);
        }
        public static void RemoveInput (string name, Action<float> action) {
            if (obj && obj.inputs.ContainsKey (name)) {
                obj.inputs[name] -= action;
                if (obj.inputs[name] == null) {
                    obj.inputs.Remove (name);
                    if (obj.inputs.Count == 0)
                        GameEvent.RemoveEvent (obj.TOnUpdate, Method.Update);
                }
            }
        }
        public static void RemoveInput (int index, Action action, MouseType type) {
            switch (type) {
                case MouseType.Down:
                    if (obj && obj.inputsMouseDown.ContainsKey (index)) {
                        obj.inputsMouseDown[index] -= action;
                        if (obj.inputsMouseDown[index] == null) {
                            obj.inputsMouseDown.Remove (index);
                            if (obj.inputsMouseDown.Count == 0)
                                GameEvent.RemoveEvent (obj.TOnUpdate_MouseDown, Method.Update);
                        }
                    }
                    break;
                case MouseType.Press:
                    if (obj && obj.inputsMousePress.ContainsKey (index)) {
                        obj.inputsMousePress[index] -= action;
                        if (obj.inputsMousePress[index] == null) {
                            obj.inputsMousePress.Remove (index);
                            if (obj.inputsMousePress.Count == 0)
                                GameEvent.RemoveEvent (obj.TOnUpdate_MousePress, Method.Update);
                        }
                    }
                    break;
                case MouseType.Up:
                    if (obj && obj.inputsMouseUp.ContainsKey (index)) {
                        obj.inputsMouseUp[index] -= action;
                        if (obj.inputsMouseUp[index] == null) {
                            obj.inputsMouseUp.Remove (index);
                            if (obj.inputsMouseUp.Count == 0)
                                GameEvent.RemoveEvent (obj.TOnUpdate_MouseUp, Method.Update);
                        }
                    }
                    break;
            }
        }
        public static void RemoveInput (InputData data) {
            RemoveInput (data.Name, data.Action);
        }
        public static void RemoveInput (params InputData[] datas) {
            foreach (var v in datas)
                RemoveInput (v);
        }
        private void OnEnable () {
            if (inputs.Count != 0)
                GameEvent.AddEvent (TOnUpdate, Method.Update);
            if (inputsMouseDown.Count != 0)
                GameEvent.AddEvent (TOnUpdate_MouseDown, Method.Update);
            if (inputsMousePress.Count != 0)
                GameEvent.AddEvent (TOnUpdate_MousePress, Method.Update);
            if (inputsMouseUp.Count != 0)
                GameEvent.AddEvent (TOnUpdate_MouseUp, Method.Update);
        }
        private void OnDisable () {
            if (inputs.Count != 0)
                GameEvent.RemoveEvent (TOnUpdate, Method.Update);
            if (inputsMouseDown.Count != 0)
                GameEvent.RemoveEvent (TOnUpdate_MouseDown, Method.Update);
            if (inputsMousePress.Count != 0)
                GameEvent.RemoveEvent (TOnUpdate_MousePress, Method.Update);
            if (inputsMouseUp.Count != 0)
                GameEvent.RemoveEvent (TOnUpdate_MouseUp, Method.Update);
        }
    }

    public enum MouseType {
        Down,
        Up,
        Press
    }
}