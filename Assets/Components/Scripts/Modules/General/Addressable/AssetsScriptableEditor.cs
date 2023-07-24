using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Unit;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace RoboFactory.General.Asset
{
    [CustomEditor(typeof(AssetsScriptable))]
    public class AssetsScriptableEditor : Editor
    {
        private AssetsScriptable _util;

        private int _rawCount;
        private int _unitsCount;
        private int _productGroupCount;
        private int _specGroupCount;

        private bool _rawIconsFoldout;
        private bool _unitsIconsFoldout;
        private bool _productGroupIconsFoldout;
        private bool _specGroupIconsFoldout;

        private void Awake()
        {
            _util = (AssetsScriptable) target;
            
            _rawCount = Enum.GetValues(typeof(RawType)).Length;
            _unitsCount = Enum.GetValues(typeof(UnitType)).Length;
            _productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length;
            _specGroupCount = Enum.GetValues(typeof(SpecType)).Length;
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
                    _util.RawIcons.Add(new RawIconObject());
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
                rawIcons[i - 1].Type = (RawType) EditorGUILayout.EnumPopup("Type: ", (RawType) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("_rawIcons")
                    .GetArrayElementAtIndex(i - 1)
                    .FindPropertyRelative("IconRef"), new GUIContent("Icon"));
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
                    _util.UnitIcons.Add(new UnitIconObject());
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
                unitTypeIcons[i].Type = (UnitType) EditorGUILayout.EnumPopup("Type: ", (UnitType) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("_unitIcons")
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative("IconRef"), new GUIContent("Icon"));
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
                    _util.ProductGroupIcons.Add(new ProductGroupIconObject());
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
                productGroupIcons[i].Type = (ProductGroup) EditorGUILayout.EnumPopup("Type: ", (ProductGroup) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("_productGroupIcons")
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative("IconRef"), new GUIContent("Icon"));
                EditorGUILayout.EndVertical();
            }
        }
        
        private void CreateSpecificationIconsSection()
        {
            var specificationIcons = _util.SpecsIcons;
            if (specificationIcons.Count < _specGroupCount)
            {
                for (var i = 0; i < _specGroupCount - specificationIcons.Count; i++)
                {
                    _util.SpecsIcons.Add(new SpecIconObject());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _specGroupIconsFoldout = EditorGUILayout.Foldout(
                _specGroupIconsFoldout, 
                $"Specification Icons: {_util.SpecsIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_specGroupIconsFoldout)
                return;
            
            for (var i = 0; i < _specGroupCount; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                specificationIcons[i].Type = (SpecType) EditorGUILayout.EnumPopup("Type: ", (SpecType) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("_specsIcons")
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative("IconRef"), new GUIContent("Icon"));
                EditorGUILayout.EndVertical();
            }
        }
    }
}
#endif