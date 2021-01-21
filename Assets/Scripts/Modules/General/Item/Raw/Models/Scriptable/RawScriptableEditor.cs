using System.Collections.Generic;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Models.Scriptable;
using Modules.General.Item.Raw.Models.Type;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Modules.General.Item.Raw.Models.Scriptable
{
    [CustomEditor(typeof(RawScriptable))]
    public class RawScriptableEditor : Editor
    {
        private RawScriptable _raw;

        private bool _recipeSectionFoldout;
        private List<bool> _recipesFoldouts;
        private List<bool> _partsFoldouts;

        private void Awake()
        {
            _recipesFoldouts ??= new List<bool>();
            _partsFoldouts ??= new List<bool>();

            _raw = (RawScriptable) target;
            _raw.Recipes ??= new List<RecipeObject>();
            
            for (var i = 0; i < _raw.Recipes.Count; i++)
            {
                _recipesFoldouts.Add(false);
                _partsFoldouts.Add(false);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            GUILayout.Label($"{_raw.RawType.ToString()}: {_raw.RawName}");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _raw.Index = EditorGUILayout.IntField("Index:", _raw.Index);
            _raw.RawName = EditorGUILayout.TextField("Name:", _raw.RawName);
            _raw.Key = EditorGUILayout.TextField("Key:", _raw.Key);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iconRef"), new GUIContent("Icon"));
            _raw.RawType = (RawType) EditorGUILayout.EnumPopup("Raw Type:", _raw.RawType);

            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            _raw.IsMain = EditorGUILayout.Toggle("Is Main Raw", _raw.IsMain);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            
            if (_raw.IsMain)
                CreateRecipeSection();
            
            EditorGUILayout.EndVertical();

            EditorUtility.SetDirty(_raw);
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateRecipeSection()
        {
            var recipes = _raw.Recipes;
            
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            _recipeSectionFoldout = EditorGUILayout.Foldout(_recipeSectionFoldout, $"Recipes: {recipes.Count}");
            EditorGUILayout.EndHorizontal();
            
            if (!_recipeSectionFoldout)
                return;

            for (var i = 0; i < recipes.Count; i++)
            {
                EditorGUI.indentLevel = 1;
                
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                _recipesFoldouts[i] = EditorGUILayout.Foldout(_recipesFoldouts[i], $"Star: {recipes[i].Star}");
                if (_recipesFoldouts[i])
                {
                    GUILayout.Space(5);
                    
                    recipes[i].Star = EditorGUILayout.IntField("Star:", recipes[i].Star);
                    recipes[i].Cost = EditorGUILayout.IntField("Craft Cost:", recipes[i].Cost);

                    GUILayout.Space(5);
                    
                    CreatePartSection(i);
                }
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.BeginHorizontal();
            if (recipes.Count != 0)
            {
                if(GUILayout.Button("Remove last recipe"))
                    RemoveLastRecipe();
            }
            if (recipes.Count < Constants.MaxStar)
            {
                if(GUILayout.Button("Add new recipe"))
                    AddRecipe();
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void AddRecipe()
        {
            _recipesFoldouts.Add(false);
            _partsFoldouts.Add(false);
            _raw.Recipes.Add(new RecipeObject
            {
                Parts = new List<PartObject>()
            });
        }

        private void RemoveLastRecipe()
        {
            _recipesFoldouts.RemoveAt(_recipesFoldouts.Count - 1);
            _raw.Recipes.RemoveAt(_raw.Recipes.Count - 1);
        }
        
        private void CreatePartSection(int index)
        {
            var parts = _raw.Recipes[index].Parts;
            
            EditorGUI.indentLevel = 0;
            
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUI.indentLevel++;
            _partsFoldouts[index] = EditorGUILayout.Foldout(_partsFoldouts[index], $"Parts count: {parts.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_partsFoldouts[index])
                return;
            
            for (var i = 0; i < parts.Count; i++)
            {
                EditorGUILayout.BeginVertical("Box");
                GUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                parts[i].data = (ItemScriptable) EditorGUILayout.ObjectField("Data:", parts[i].data, typeof(ItemScriptable), true);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                parts[i].count = EditorGUILayout.IntField("Count:", parts[i].count);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                parts[i].star = EditorGUILayout.IntField("Star:", parts[i].star);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);
                EditorGUILayout.EndVertical();

                if (i != parts.Count - 1)
                    GUILayout.Space(1);
            }
            
            EditorGUI.indentLevel = 0;
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