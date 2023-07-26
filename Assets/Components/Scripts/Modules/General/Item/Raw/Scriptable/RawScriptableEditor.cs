using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace RoboFactory.General.Item.Raw
{
    [CustomEditor(typeof(RawScriptable))]
    public class RawScriptableEditor : Editor
    {
        private RawScriptable _raw;

        private bool _recipeSectionFoldout;

        private void Awake()
        {
            _raw = (RawScriptable) target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            GUILayout.Label($"{_raw.RawType.ToString()}: {_raw.RawName}");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _raw.Index = EditorGUILayout.IntField("Index:", _raw.Index);
            _raw.RawName = EditorGUILayout.TextField("Name:", _raw.RawName);
            _raw.Key = EditorGUILayout.TextField("Key:", _raw.Key);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_iconRef"), new GUIContent("Icon"));
            _raw.RawType = (RawType) EditorGUILayout.EnumPopup("Raw Type:", _raw.RawType);
            EditorGUILayout.EndVertical();
            
            CreateRecipeSection();

            EditorUtility.SetDirty(_raw);
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateRecipeSection()
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            _recipeSectionFoldout = EditorGUILayout.Foldout(_recipeSectionFoldout, "Recipe");
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
            
            if (!_recipeSectionFoldout)
                return;

            CreatePartSection();
            EditorGUILayout.EndHorizontal();
        }

        private void CreatePartSection()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _raw.Recipe.Star = EditorGUILayout.IntField("Star:", _raw.Recipe.Star);
            _raw.Recipe.Cost = EditorGUILayout.IntField("Craft Cost:", _raw.Recipe.Cost);

            GUILayout.Space(5);
            
            var parts = _raw.Recipe.Parts;
            for (var i = 0; i < parts.Count; i++)
            {
                EditorGUILayout.BeginVertical("Box");
                GUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                parts[i].Data = (ItemScriptable) EditorGUILayout.ObjectField("Data:", parts[i].Data, typeof(ItemScriptable), true);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                parts[i].Count = EditorGUILayout.IntField("Count:", parts[i].Count);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                parts[i].Star = EditorGUILayout.IntField("Star:", parts[i].Star);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);
                EditorGUILayout.EndVertical();

                if (i != parts.Count - 1)
                    GUILayout.Space(1);
            }
            
            if (parts.Count != 0)
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            EditorGUILayout.BeginHorizontal();
            if (parts.Count != 0)
            {
                if (GUILayout.Button("Remove last part"))
                    RemoveLastPart(parts);
            }

            if (parts.Count < 2)
            {
                if(GUILayout.Button("Add new part"))
                    AddPart(parts);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel = 1;
        }
        
        private void AddPart(List<PartObject> parts)
        {
            parts.Add(new PartObject());
        }
        private void RemoveLastPart(List<PartObject> parts)
        {
            parts.RemoveAt(parts.Count - 1);
        }
    }
}
#endif