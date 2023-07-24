using System;
using RoboFactory.General.Unit;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace RoboFactory.General.Item.Products
{
    [CustomEditor(typeof(EngineProductScriptable))]
    public class EngineProductScriptableEditor : ProductScriptableEditorBase
    {
        private EngineProductScriptable _productData;
        
        private bool _infoFoldout = true;
        private bool _recipeFoldout;
        private bool _specificationsFoldout;

        private void Awake()
        {
            _productData = (EngineProductScriptable) target;
            
            EditorUtility.SetDirty(_productData);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CreateProduct();
            CreateRecipe();
            CreateSpecs();

            EditorUtility.SetDirty(_productData);
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateProduct()
        {
            var engineArray = Enum.GetNames(typeof(EngineType));
            
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUILayout.LabelField(_productData.ProductName);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _infoFoldout = EditorGUILayout.Foldout(_infoFoldout, "Info");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();

            if (!_infoFoldout)
                return;
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _productData.Index = EditorGUILayout.IntField("Index:", _productData.Index);
            _productData.ProductName = EditorGUILayout.TextField("Name:", _productData.ProductName);
            _productData.Key = EditorGUILayout.TextField("Key:", _productData.Key);
            _productData.Star = EditorGUILayout.IntField("Star:", _productData.Star);
            EditorGUILayout.PropertyField(serializedObject
                .FindProperty("_iconRef"), new GUIContent("Icon"));
            
            _productData.Model = (GameObject)EditorGUILayout.ObjectField(
                    "Model:", _productData.Model, typeof(GameObject), false);
            
            EditorGUILayout.Space(10);
            EditorGUI.BeginDisabledGroup(true);
            _productData.ItemType = (ItemType) EditorGUILayout.EnumPopup("Item Type:", _productData.ItemType);
            _productData.ProductGroup = (ProductGroup) EditorGUILayout.EnumPopup("Product Group:", _productData.ProductGroup);
            EditorGUI.EndDisabledGroup();
            _productData.UnitType = (UnitType) EditorGUILayout.EnumPopup("Character Type:", _productData.UnitType);
            _productData.ProductType = EditorGUILayout.Popup("Equipment Type:", _productData.ProductType, engineArray);
            _productData.IsProduct = EditorGUILayout.Toggle("Is product:", _productData.IsProduct);

            EditorGUILayout.EndVertical();
        }

        private void CreateRecipe()
        {
            if (_productData.ProductType == 0)
                return;
            
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _recipeFoldout = EditorGUILayout.Foldout(_recipeFoldout, "Recipe");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();

            if (!_recipeFoldout)
                return;
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            _productData.Recipe.Cost = EditorGUILayout.IntField("Cost:", _productData.Recipe.Cost);
            EditorGUILayout.EndHorizontal();
                    
            EditorGUILayout.BeginHorizontal();
            _productData.Recipe.Experience = EditorGUILayout.IntField("Experience:", _productData.Recipe.Experience);
            EditorGUILayout.EndHorizontal();
                    
            EditorGUILayout.BeginHorizontal();
            _productData.Recipe.CraftTime = EditorGUILayout.IntField("Craft time:", _productData.Recipe.CraftTime);
            EditorGUILayout.EndHorizontal();
                    
            GUILayout.Space(5);
                    
            CreateParts();
            EditorGUILayout.EndVertical();
        }

        private void CreateParts()
        {
            var parts = _productData.Recipe.Parts;
            EditorGUI.indentLevel = 0;

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

            if (parts.Count < Constants.MaxPart)
            {
                if(GUILayout.Button("Add new part"))
                    AddPart(parts);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void CreateSpecs()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _specificationsFoldout = EditorGUILayout.Foldout(_specificationsFoldout, "Specifications");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();

            if (!_specificationsFoldout)
                return;
            
            var specs = _productData.Recipe.Specs;
            var specTypeCount = Enum.GetValues(typeof(SpecType)).Length;
            if (specs.Count < specTypeCount)
            {
                for (var i = specs.Count; i < specTypeCount; i++)
                {
                    specs.Add(new SpecObject { Type = (SpecType) i });
                }
            }

            foreach (var spec in specs)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginDisabledGroup(true);
                spec.Type = (SpecType) EditorGUILayout.EnumPopup("Type:", spec.Type);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                spec.Value = EditorGUILayout.IntField("Value:", spec.Value);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
    }
}
#endif