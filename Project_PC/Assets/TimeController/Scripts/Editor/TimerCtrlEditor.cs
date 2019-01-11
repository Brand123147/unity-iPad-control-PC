using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimerCtrl))]
public class TimerCtrlEditor : Editor
{
    TimerCtrl _target;
    // Use this for initialization
    void OnEnable()
    {
        _target = (TimerCtrl)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Undo.RecordObject(_target, "target change");

        //GUI.backgroundColor = Color.cyan;//背景颜色
        GUI.color = Color.green;//整体颜色
        //GUI.contentColor = Color.yellow;//字体颜色

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PrefixLabel("Timer");
        GUILayout.Label("Year");
        _target.mTimer.year = EditorGUILayout.IntField(_target.mTimer.year);
        GUILayout.Label("Month");
        _target.mTimer.month = EditorGUILayout.IntField(_target.mTimer.month);
        GUILayout.Label("Day");
        _target.mTimer.day = EditorGUILayout.IntField(_target.mTimer.day);

        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
