using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Forest))]
public class ForestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Forest _target = (Forest)target;
        if (target == null) return;
        Undo.RecordObject(_target, "Change Forest Editor");

        if (GUILayout.Button("Generate"))
        {
            _target.GenArrays();
        }
    }
}
