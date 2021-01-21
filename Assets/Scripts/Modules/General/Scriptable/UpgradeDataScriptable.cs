using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.General.Scriptable
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable/General/Upgrade Data", order = 1)]
    public class UpgradeDataScriptable : ScriptableObject
    {
        [SerializeField] private List<UpgradeDataObject> data;
        
        public List<UpgradeDataObject> Data { get => data; set => data = value; }
    }
    
    [Serializable]
    public class UpgradeDataObject
    {
        [SerializeField] private int count;
        [SerializeField] private int cost;
        [SerializeField] private int level;
        
        public int Count { get => count; set => count = value; }
        public int Cost { get => cost; set => cost = value; }
        public int Level { get => level; set => level = value; }
    }
}
