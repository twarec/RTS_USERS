using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace YG_EventSystem
{
[CustomEditor(typeof(GameEvent))]
public class GamaManagerEditor : Editor
{
    private AnimBool animUpdate;
    private AnimBool animFixedUpdate;
    private AnimBool animLateUpdate;


    private void OnEnable()
    {
        animUpdate = new AnimBool(true);
        animUpdate.valueChanged.AddListener(Repaint);
        animFixedUpdate = new AnimBool(true);
        animFixedUpdate.valueChanged.AddListener(Repaint);
        animLateUpdate = new AnimBool(true);
        animLateUpdate.valueChanged.AddListener(Repaint);
    }
    public override void OnInspectorGUI()
    {
        GameEvent ge = (GameEvent)target;

        EditorGUILayout.LabelField("Count event Update : " + (ge.updateEvent == null ? 0 : ge.updateEvent.GetInvocationList().Length));
        EditorGUILayout.LabelField("Time event Update : " + ge.UpdateTime.ToString());
        EditorGUILayout.BeginVertical("box");
        animUpdate.target = EditorGUILayout.ToggleLeft("Targets event Update", animUpdate.target);
        if (EditorGUILayout.BeginFadeGroup(animUpdate.faded))
        {
            if (ge.updateEvent != null)
                foreach (var v in ge.updateEvent.GetInvocationList())
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.LabelField($"Target : {v.Target.ToString()}");
                    EditorGUILayout.LabelField($"Method : {v.Method.ToString()}");
                    EditorGUILayout.EndVertical();
                }
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Count event LateUpdate : " + (ge.lateUpdateEvent == null ? 0 : ge.lateUpdateEvent.GetInvocationList().Length));
        EditorGUILayout.LabelField("Time event LateUpdate : " + ge.LateUpdateTime.ToString());
        EditorGUILayout.BeginVertical("box");
        animLateUpdate.target = EditorGUILayout.ToggleLeft("Targets event LateUpdate", animLateUpdate.target);
        if (EditorGUILayout.BeginFadeGroup(animLateUpdate.faded))
        {
            if (ge.lateUpdateEvent != null)
                foreach (var v in ge.lateUpdateEvent.GetInvocationList())
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.LabelField($"Target : {v.Target.ToString()}");
                    EditorGUILayout.LabelField($"Method : {v.Method.ToString()}");
                    EditorGUILayout.EndVertical();
                }
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Count event FixedUpdate : " + (ge.fixedUpdateEvent == null ? 0 : ge.fixedUpdateEvent.GetInvocationList().Length));
        EditorGUILayout.LabelField("Time event FixedUpdate : " + ge.FixedUpdateTime.ToString());
        EditorGUILayout.BeginVertical("box");
        animFixedUpdate.target = EditorGUILayout.ToggleLeft("Targets event FixedUpdate", animFixedUpdate.target);
        if (EditorGUILayout.BeginFadeGroup(animFixedUpdate.faded))
        {
            if (ge.fixedUpdateEvent != null)
                foreach (var v in ge.fixedUpdateEvent.GetInvocationList())
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.LabelField($"Target : {v.Target.ToString()}");
                    EditorGUILayout.LabelField($"Method : {v.Method.ToString()}");
                    EditorGUILayout.EndVertical();
                }
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.EndVertical();
    }

    private void ProgressBar(float value, string str)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, str);
        EditorGUILayout.Space();
    }
}
}
