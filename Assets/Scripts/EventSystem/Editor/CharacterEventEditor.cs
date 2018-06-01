using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterEvent))]
public class CharacterEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        CharacterEvent e = target as CharacterEvent;
        if (GUILayout.Button("Trigger"))
            e.Raise(DummyCharacter.NewDummy);
    }
}
