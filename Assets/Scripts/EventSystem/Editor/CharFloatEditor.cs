using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharFloatEvent))]
public class CharFloatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        CharFloatEvent e = target as CharFloatEvent;
        if (GUILayout.Button("Trigger"))
            e.Raise();
    }
}
