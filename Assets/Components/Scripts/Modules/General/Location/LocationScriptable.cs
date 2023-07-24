using System;
using System.Collections.Generic;
using RoboFactory.General.Item;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.AddressableAssets;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Location
{
    [CreateAssetMenu(menuName = "Scriptable/Battle/Data", order = 11)]
    public class LocationScriptable : ScriptableObject
    {
        [SerializeField] private string _key;
        [SerializeField] private int _time;
        [SerializeField] private AssetReference _iconRef;
        [SerializeField] private List<LocationUnitData> _enemies;
        [SerializeField] private List<PartObject> _reward;
        
        public string Key => _key;
        public int Time => _time;
        public AssetReference IconRef => _iconRef;
        public List<LocationUnitData> Enemies => _enemies;
        public List<PartObject> Reward => _reward;
    }
    
    [Serializable]
    public class LocationsScriptable
    {
        [SerializeField] private int _star;
        [SerializeField] private int _time;
        [SerializeField] private List<LocationUnitData> _enemies;
        [SerializeField] private List<PartObject> _reward;
        
        public int Star => _star;
        public int Time => _time;
        public List<LocationUnitData> Enemies => _enemies;
        public List<PartObject> Reward => _reward;
    }

    [Serializable]
    public class LocationUnitData
    {
        public UnitScriptable Data;
        public int Place;
    }
}