using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
namespace RoboFactory.General.Scriptable
{
    [CustomEditor(typeof(UpgradeDataScriptable))]
    public class UpgradeDataScriptableEditor : Editor
    {
        private UpgradeDataScriptable _data;

        private void Awake()
        {
            _data = (UpgradeDataScriptable) target;
            _data.Data ??= new List<UpgradeDataObject>();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUILayout.LabelField("Upgrade Data");
            EditorGUILayout.EndHorizontal();

            for (var i = 0; i < _data.Data.Count; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                _data.Data[i].Count = i + 1;
                _data.Data[i].Cost = EditorGUILayout.IntField("Cost: ", _data.Data[i].Cost);
                _data.Data[i].Level = EditorGUILayout.IntField("Unlock Level: ", _data.Data[i].Level);
                EditorGUILayout.EndVertical();
            }

            EditorUtility.SetDirty(_data);
        }
    }
}
#endif