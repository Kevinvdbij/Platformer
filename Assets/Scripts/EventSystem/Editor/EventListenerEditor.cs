using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventListener))]
public class EventListenerEditor : Editor
{
    private SerializedProperty gameEvent;
    private SerializedProperty response;
    private SerializedProperty floatResponse;
    private SerializedProperty characterResponse;
    private SerializedProperty charFloatResponse;

    private void OnEnable()
    {
        gameEvent = serializedObject.FindProperty("gameEvent");
        response = serializedObject.FindProperty("response");
        floatResponse = serializedObject.FindProperty("floatResponse");
        characterResponse = serializedObject.FindProperty("characterResponse");
        charFloatResponse = serializedObject.FindProperty("charFloatResponse");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(gameEvent);
        GameEventListener e = target as GameEventListener;

        if (e.gameEvent as FloatEvent)
            EditorGUILayout.PropertyField(floatResponse);
        else if (e.gameEvent as CharacterEvent)
            EditorGUILayout.PropertyField(characterResponse);
        else if (e.gameEvent as CharFloatEvent)
            EditorGUILayout.PropertyField(charFloatResponse);
        else if (e.gameEvent != null)
        {
            EditorGUILayout.PropertyField(response);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
