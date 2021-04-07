using Menu;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelsGenerator))]
    public class LevelsGeneratorEditor : UnityEditor.Editor
    {
        private const float ReorderableListHeight = 55f;
        private const string LevelsLabel = "Levels";
        private const string LevelNumberPropertyPath = "Number";
        private const string LevelStarsRequiredPropertyPath = "StarsRequired";
        private const string LevelIdPropertyPath = "LevelId";

        private LevelsGenerator _levelsGenerator;
        private ReorderableList reorderableList;
        private SerializedProperty _levelsData;
        private SerializedProperty _content;
        private SerializedProperty _level;
        private SerializedProperty _gameDataService;

        private void OnEnable()
        {
            _levelsGenerator = (LevelsGenerator) target;

            SetSerializedObjectProperty();

            CreateReorderableList();
            DrawReorderableListHeader();
            DrawReorderableListElements();
        }

        private void SetSerializedObjectProperty()
        {
            _level = serializedObject.FindProperty(nameof(_level));
            _content = serializedObject.FindProperty(nameof(_content));
            _gameDataService = serializedObject.FindProperty(nameof(_gameDataService));
            _levelsData = serializedObject.FindProperty(nameof(_levelsData));
        }

        private void DrawReorderableListHeader()
        {
            reorderableList.drawHeaderCallback = rect => { EditorGUI.LabelField(rect, LevelsLabel, EditorStyles.boldLabel); };
        }

        private void DrawReorderableListElements()
        {
            reorderableList.drawElementCallback = (rect, index, active, focused) =>
            {
                SerializedProperty levelInfoProp = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty levelStarsRequired = levelInfoProp.FindPropertyRelative(LevelStarsRequiredPropertyPath);
                SerializedProperty levelId = levelInfoProp.FindPropertyRelative(LevelIdPropertyPath);

                rect.height = EditorGUIUtility.singleLineHeight;
                string label = $"Level {index + 1}";
                EditorGUI.LabelField(rect, label);
                rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(rect, levelStarsRequired);
                rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(rect, levelId);
            };
        }

        private void CreateReorderableList()
        {
            reorderableList = new ReorderableList(serializedObject, _levelsData, false, true, true, true)
            {
                elementHeight = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) +
                                ReorderableListHeight
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_level);
            EditorGUILayout.PropertyField(_content);
            EditorGUILayout.PropertyField(_gameDataService);
            reorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                SetObjectDirty();
            }
        }

        private void SetObjectDirty()
        {
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(_levelsGenerator.gameObject.scene);
        }
    }
}