using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Type;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Modules.General.Unit.Scriptable
{
    [CustomEditor(typeof(UnitScriptable))]
    public class UnitScriptableEditor : Editor
    {
        private UnitScriptable _unit;

        private void Awake()
        {
            _unit = (UnitScriptable) target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _unit.Key = EditorGUILayout.TextField("Key:", _unit.Key);
            _unit.UnitType = (UnitType) EditorGUILayout.EnumPopup("Unit Type:", _unit.UnitType);
            _unit.AttackType = (AttackType) EditorGUILayout.EnumPopup("Attack Type:", _unit.AttackType);
            _unit.SpecType = (SpecType) EditorGUILayout.EnumPopup("Specification Type:", _unit.SpecType);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iconRef"), new GUIContent("Icon"));
            _unit.Model = (GameObject) EditorGUILayout.ObjectField("Model:", _unit.Model, typeof(GameObject), false);
            EditorGUILayout.EndVertical();

            CreateSpecificationSection();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _unit.UnlockCost = EditorGUILayout.IntField("Unlock Cost:", _unit.UnlockCost);
            _unit.UnlockLevel = EditorGUILayout.IntField("Unlock Level:", _unit.UnlockLevel);
            EditorGUILayout.EndVertical();

            EditorUtility.SetDirty(_unit);
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void CreateSpecificationSection()
        {
            var specifications = _unit.Specifications;
            if (specifications.Count < 4)
            {
                for (var i = specifications.Count; i < 4; i++)
                {
                    _unit.Specifications.Add(new int());
                }
            }
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _unit.Specifications[0] = EditorGUILayout.IntField("Attack:", _unit.Specifications[0]);
            _unit.Specifications[1] = EditorGUILayout.IntField("Health:", _unit.Specifications[1]);
            _unit.Specifications[2] = EditorGUILayout.IntField("Defense:", _unit.Specifications[2]);
            _unit.Specifications[3] = EditorGUILayout.IntField("Initiative:", _unit.Specifications[3]);
            EditorGUILayout.EndVertical();
        }
    }
}
#endif