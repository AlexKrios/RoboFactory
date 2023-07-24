using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoboFactory.General.Scriptable
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable/General/Upgrade Data", order = 1)]
    public class UpgradeDataScriptable : ScriptableObject
    {
        [SerializeField] private List<UpgradeDataObject> _data;
        
        public List<UpgradeDataObject> Data { get => _data; set => _data = value; }
    }
    
    [Serializable]
    public class UpgradeDataObject
    {
        [SerializeField] private int _count;
        [SerializeField] private int _cost;
        [SerializeField] private int _level;
        
        public int Count { get => _count; set => _count = value; }
        public int Cost { get => _cost; set => _cost = value; }
        public int Level { get => _level; set => _level = value; }
    }
}
