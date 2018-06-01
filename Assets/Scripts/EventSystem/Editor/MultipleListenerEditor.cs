using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MultipleEventListener))]
public class MultipleEventListenerEditor : Editor
{
    private ReorderableList list;
    private SerializedProperty response;

    private void OnEnable()
    {
        response = serializedObject.FindProperty("response");
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("events"), true, true, true, true);

        list.drawHeaderCallback = rect =>
        {
            Rect foldoutLabel = new Rect(rect.x, rect.y, rect.width, rect.height);
            GUI.Label(foldoutLabel, "Events");
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);

            Rect box = rect;
            box.height -= 3;
            box.y += 1;
            EditorGUI.ObjectField(box, element, new GUIContent(""));
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.Space();
        list.DoLayoutList();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(response);
        serializedObject.ApplyModifiedProperties();
    }
}