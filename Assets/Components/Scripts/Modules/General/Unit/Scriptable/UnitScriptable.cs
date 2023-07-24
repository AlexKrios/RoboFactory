using System.Collections.Generic;
using RoboFactory.General.Item.Products;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoboFactory.General.Unit
{
    [CreateAssetMenu(menuName = "Scriptable/General/Unit/Data", order = 81)]
    public class UnitScriptable : ScriptableObject
    {
        [SerializeField] private string _key;
        
        [SerializeField] private UnitType _unitType;
        [SerializeField] private AttackType _attackType;
        [SerializeField] private SpecType _specType;
        
        [SerializeField] private AssetReference _iconRef;
        [SerializeField] private GameObject _model;

        [SerializeField] private List<int> _specs;
        
        [SerializeField] private int _unlockCost;
        [SerializeField] private int _unlockLevel;

        public string Key { get => _key; set => _key = value; }
        
        public UnitType UnitType { get => _unitType; set => _unitType = value; }
        public AttackType AttackType { get => _attackType; set => _attackType = value; }
        public SpecType SpecType { get => _specType; set => _specType = value; }
        
        public AssetReference IconRef { get => _iconRef; set => _iconRef = value; }
        public GameObject Model { get => _model; set => _model = value; }
        
        public List<int> Specifications { get => _specs; set => _specs = value; }
        
        public int UnlockCost { get => _unlockCost; set => _unlockCost = value; }
        public int UnlockLevel { get => _unlockLevel; set => _unlockLevel = value; }
    }
}