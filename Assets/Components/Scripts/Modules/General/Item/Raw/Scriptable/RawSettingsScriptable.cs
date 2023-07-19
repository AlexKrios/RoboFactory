using System.Collections.Generic;
using UnityEngine;

namespace RoboFactory.General.Item.Raw
{
    [CreateAssetMenu(fileName = "RawSettings", menuName = "Scriptable/General/Raw/Settings", order = 62)]
    public class RawSettingsScriptable : ScriptableObject
    {
        [SerializeField] private List<int> _cap;
        
        public List<int> Cap => _cap;
    }
}