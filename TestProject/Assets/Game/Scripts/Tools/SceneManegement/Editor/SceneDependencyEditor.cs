using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

namespace Tools
{
    [CustomEditor(typeof(SceneDependency))]
    public class SceneDependencyEditor : Editor
    {
        private SceneDependency _core;
        private string[] _scenes;
        SerializedProperty _dependencyScenes;
        ReorderableList _dependencyList;

        public void OnEnable()
        {
            _core = (SceneDependency)target;
            _scenes = GetSceneList();
            _dependencyScenes = serializedObject.FindProperty("dependencyScenes");

            _dependencyList = new ReorderableList(serializedObject, _dependencyScenes, true, true, true, true);
            _dependencyList.drawElementCallback = DrawElement;
            _dependencyList.drawHeaderCallback = DrawHeader;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();

            GUILayout.Label("Start scene:");
            _core.initSceneNum = EditorGUILayout.Popup(_core.initSceneNum, _scenes);
            GUILayout.Label("Scenes dependencies:");
            _dependencyList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }


        public string[] GetSceneList()
        {
            List<string> _list = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                Debug.Log(EditorBuildSettings.scenes[i].path);
                _list.Add(EditorBuildSettings.scenes[i].path);
            }
            return _list.ToArray();
        }

        private void DrawElement(Rect rect, int i, bool isActive, bool isFocused)
        {
            var element = _dependencyScenes.GetArrayElementAtIndex(i);
            rect.y += 2;

            EditorGUI.LabelField(new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight), (i + 1).ToString());
            EditorGUI.PropertyField(new Rect(rect.x + 21, rect.y, 200, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            //EditorGUI.PropertyField(new Rect(rect.x + 225, rect.y, 120, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("data"), GUIContent.none);
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Scenes");
        }
    }
}
