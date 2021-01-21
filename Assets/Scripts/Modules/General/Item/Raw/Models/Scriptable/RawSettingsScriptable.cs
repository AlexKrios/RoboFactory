using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.General.Item.Raw.Models.Scriptable
{
    [CreateAssetMenu(fileName = "RawSettings", menuName = "Scriptable/General/Raw/Settings", order = 62)]
    public class RawSettingsScriptable : ScriptableObject
    {
        [SerializeField] private List<RawSettingsObject> settings;

        public List<RawSettingsObject> Settings => settings;
    }
    
    [Serializable]
    public class RawSettingsObject
    {
        public int cap;
    }
}