using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (RTS.SelectebleObjectManager))]
public class SelecteblemanagerEditor : Editor {
    private RTS.SelectebleObjectManager SOM;
    public override void OnInspectorGUI () {
        base.OnInspectorGUI ();

        SOM = (RTS.SelectebleObjectManager) target;

        EditorGUILayout.LabelField($"Selection object");
        foreach (var v in SOM.SelectebleObjects) {
            EditorGUILayout.BeginVertical ("box");
            EditorGUILayout.LabelField ($"Tag => {v.Tag}");
            EditorGUILayout.LabelField ($"Name => {v.Name}");
            EditorGUILayout.EndVertical ();
        }
        EditorGUILayout.LabelField($"All Selection object");
        foreach (var v in SOM.AllSelctioObjects)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Tag => {v.Tag}");
            EditorGUILayout.LabelField($"Name => {v.Name}");
            EditorGUILayout.EndVertical();
        }

        EditorUtility.SetDirty (SOM);
    }
}