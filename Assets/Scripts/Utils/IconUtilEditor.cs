using System;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Item.Raw.Models.Type;
using Modules.General.Unit.Models.Type;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Utils
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
                for (var i = 0; i < _rawCount - rawIcons.Count; i++)
                {
                    _util.RawIcons.Add(new RawIconKeyObject());
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
            for (var i = 0; i < _rawCount; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                rawIcons[i].type = (RawType) EditorGUILayout.EnumPopup("Type: ", (RawType) i);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.PropertyField(serializedObject
                    .FindProperty("rawIcons")
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative("iconRef"), new GUIContent("Icon"));
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateUnitTypeIconsSection()
        {
            var unitTypeIcons = _util.UnitIcons;
            if (unitTypeIcons.Count < _unitsCount)
            {
                for (var i = 0; i < _unitsCount - unitTypeIcons.Count; i++)
                {
                    _util.UnitIcons.Add(new UnitTypeIconKeyObject());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _unitsIconsFoldout = EditorGUILayout.Foldout(_unitsIconsFoldout, $"Units Icons: {_util.UnitIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_unitsIconsFoldout)
                return;
            
            for (var i = 0; i < _unitsCount; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                unitTypeIcons[i].type = (UnitType) EditorGUILayout.EnumPopup("Type: ", (UnitType) i);
                EditorGUI.EndDisabledGroup();
                unitTypeIcons[i].icon = (Sprite) EditorGUILayout.ObjectField("Icon:", unitTypeIcons[i].icon, typeof(Sprite), false);
                EditorGUILayout.EndVertical();
            }
        }
        
        private void CreateProductGroupIconsSection()
        {
            var productGroupIcons = _util.ProductGroupIcons;
            if (productGroupIcons.Count < _productGroupCount)
            {
                for (var i = 0; i < _productGroupCount - productGroupIcons.Count; i++)
                {
                    _util.ProductGroupIcons.Add(new ProductGroupIconKeyObject());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _productGroupIconsFoldout = EditorGUILayout.Foldout(_productGroupIconsFoldout, $"Product Group Icons: {_util.ProductGroupIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_productGroupIconsFoldout)
                return;
            
            for (var i = 0; i < _productGroupCount; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                productGroupIcons[i].type = (ProductGroup) EditorGUILayout.EnumPopup("Type: ", (ProductGroup) i);
                EditorGUI.EndDisabledGroup();
                productGroupIcons[i].icon = (Sprite) EditorGUILayout.ObjectField("Icon:", productGroupIcons[i].icon, typeof(Sprite), false);
                EditorGUILayout.EndVertical();
            }
        }
        
        private void CreateSpecificationIconsSection()
        {
            var specificationIcons = _util.SpecificationIcons;
            if (specificationIcons.Count < _specificationGroupCount)
            {
                for (var i = 0; i < _specificationGroupCount - specificationIcons.Count; i++)
                {
                    _util.SpecificationIcons.Add(new SpecificationIconKeyObject());
                }
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUI.indentLevel++;
            _specificationGroupIconsFoldout = EditorGUILayout.Foldout(_specificationGroupIconsFoldout, $"Specification Icons: {_util.SpecificationIcons.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_specificationGroupIconsFoldout)
                return;
            
            for (var i = 0; i < _specificationGroupCount; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.BeginDisabledGroup(true);
                specificationIcons[i].type = (SpecType) EditorGUILayout.EnumPopup("Type: ", (SpecType) i);
                EditorGUI.EndDisabledGroup();
                specificationIcons[i].icon = (Sprite) EditorGUILayout.ObjectField("Icon:", specificationIcons[i].icon, typeof(Sprite), false);
                EditorGUILayout.EndVertical();
            }
        }
    }
}
#endif