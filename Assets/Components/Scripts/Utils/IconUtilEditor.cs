using System;
using Components.Scripts.Modules.General;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Unit.Type;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

#if UNITY_EDITOR
namespace Components.Scripts.Utils
{
    [CustomEditor(typeof(IconUtil))]
    public class IconUtilEditor : Editor
    {
        private IconUtil _util;

        private int _rawCount;
        private int _unitsCount;
        private int _productGroupCount;
        private int _specificationGroupCount;

        private bool _rawIconsFoldout;
        private bool _unitsIconsFoldout;
        private bool _productGroupIconsFoldout;
        private bool _specificationGroupIconsFoldout;

        private void Awake()
        {
            _util = (IconUtil) target;
            
            _rawCount = Enum.GetValues(typeof(RawType)).Length;
            _unitsCount = Enum.GetValues(typeof(UnitType)).Length;
            _productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length;
            _specificationGroupCount = Enum.GetValues(typeof(SpecType)).Length;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            CreateRawIconsSection();
            CreateUnitTypeIconsSection();
            CreateProductGroupIconsSection();
            CreateSpecificationIconsSection();

            serializedObject.ApplyModifiedProperties();
        }

        private void CreateRawIconsSection()
        {
            var rawIcons = _util.RawIcons;
            if (rawIcons.Count < _rawCount)
            {
                for (var i = 1; i < _rawCount - rawIcons.Count - 1; i++)
                {
                    _util.RawIcons.Add(new KeyValuePair<RawType, AssetReference>());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _rawIconsFoldout = EditorGUILayout.Foldout(_rawIconsFoldout, $"Raw Icons: {_util.RawIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_rawIconsFoldout)
                return;
            
            serializedObject.Update();
            for (var i = 1; i < _rawCount - 1; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                rawIcons[i - 1].Key = (RawType) EditorGUILayout.EnumPopup("Type: ", (RawType) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("rawIcons")
                    .GetArrayElementAtIndex(i - 1)
                    .FindPropertyRelative("value"), new GUIContent("Icon"));
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateUnitTypeIconsSection()
        {
            var unitTypeIcons = _util.UnitIcons;
            if (unitTypeIcons.Count < _unitsCount)
            {
                for (var i = 0; i < _unitsCount - unitTypeIcons.Count - 1; i++)
                {
                    _util.UnitIcons.Add(new KeyValuePair<UnitType, AssetReference>());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _unitsIconsFoldout = EditorGUILayout.Foldout(_unitsIconsFoldout, $"Units Icons: {_util.UnitIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_unitsIconsFoldout)
                return;
            
            for (var i = 0; i < _unitsCount - 1; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                unitTypeIcons[i].Key = (UnitType) EditorGUILayout.EnumPopup("Type: ", (UnitType) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("unitIcons")
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative("value"), new GUIContent("Icon"));
                EditorGUILayout.EndVertical();
            }
        }
        
        private void CreateProductGroupIconsSection()
        {
            var productGroupIcons = _util.ProductGroupIcons;
            if (productGroupIcons.Count < _productGroupCount)
            {
                for (var i = 0; i < _productGroupCount - productGroupIcons.Count - 1; i++)
                {
                    _util.ProductGroupIcons.Add(new KeyValuePair<ProductGroup, AssetReference>());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _productGroupIconsFoldout = EditorGUILayout.Foldout(_productGroupIconsFoldout, $"Product Group Icons: {_util.ProductGroupIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_productGroupIconsFoldout)
                return;
            
            for (var i = 0; i < _productGroupCount - 1; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                productGroupIcons[i].Key = (ProductGroup) EditorGUILayout.EnumPopup("Type: ", (ProductGroup) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("productGroupIcons")
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative("value"), new GUIContent("Icon"));
                EditorGUILayout.EndVertical();
            }
        }
        
        private void CreateSpecificationIconsSection()
        {
            var specificationIcons = _util.SpecsIcons;
            if (specificationIcons.Count < _specificationGroupCount)
            {
                for (var i = 0; i < _specificationGroupCount - specificationIcons.Count; i++)
                {
                    _util.SpecsIcons.Add(new KeyValuePair<SpecType, AssetReference>());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _specificationGroupIconsFoldout = EditorGUILayout.Foldout(
                _specificationGroupIconsFoldout, 
                $"Specification Icons: {_util.SpecsIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_specificationGroupIconsFoldout)
                return;
            
            for (var i = 0; i < _specificationGroupCount; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                specificationIcons[i].Key = (SpecType) EditorGUILayout.EnumPopup("Type: ", (SpecType) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("specsIcons")
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative("value"), new GUIContent("Icon"));
                EditorGUILayout.EndVertical();
            }
        }
    }
}
#endif