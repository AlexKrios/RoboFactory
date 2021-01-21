using System;
using System.Collections.Generic;
using Modules.General.Item.Models.Recipe;
using Modules.General.Unit.Models.Scriptable;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.General.Location
{
    [CreateAssetMenu(menuName = "Scriptable/Battle/Data", order = 11)]
    public class LocationScriptable : ScriptableObject
    {
        [SerializeField] private string key;
        [SerializeField] private int time;
        [SerializeField] private AssetReference iconRef;
        [SerializeField] private List<LocationUnitData> enemies;
        [SerializeField] private List<PartObject> reward;
        
        public string Key => key;
        public int Time => time;
        public AssetReference IconRef => iconRef;
        public List<LocationUnitData> Enemies => enemies;
        public List<PartObject> Reward => reward;
    }
    
    [Serializable]
    public class LocationsScriptable
    {
        [SerializeField] private int star;
        [SerializeField] private int time;
        [SerializeField] private List<LocationUnitData> enemies;
        [SerializeField] private List<PartObject> reward;
        
        public int Star => star;
        public int Time => time;
        public List<LocationUnitData> Enemies => enemies;
        public List<PartObject> Reward => reward;
    }

    [Serializable]
    public class LocationUnitData
    {
        public UnitScriptable data;
        public int place;
    }
}