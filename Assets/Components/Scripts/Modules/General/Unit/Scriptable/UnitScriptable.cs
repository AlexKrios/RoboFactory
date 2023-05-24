using System.Collections.Generic;
using RoboFactory.General.Item.Products;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoboFactory.General.Unit
{
    [CreateAssetMenu(menuName = "Scriptable/General/Unit/Data", order = 81)]
    public class UnitScriptable : ScriptableObject
    {
        [SerializeField] private string key;
        
        [SerializeField] private UnitType unitType;
        [SerializeField] private AttackType attackType;
        [SerializeField] private SpecType specType;
        
        [SerializeField] private AssetReference iconRef;
        [SerializeField] private GameObject model;

        [SerializeField] private List<int> specifications;
        
        [SerializeField] private int unlockCost;
        [SerializeField] private int unlockLevel;

        public string Key { get => key; set => key = value; }
        
        public UnitType UnitType { get => unitType; set => unitType = value; }
        public AttackType AttackType { get => attackType; set => attackType = value; }
        public SpecType SpecType { get => specType; set => specType = value; }
        
        public AssetReference IconRef { get => iconRef; set => iconRef = value; }
        public GameObject Model { get => model; set => model = value; }
        
        public List<int> Specifications { get => specifications; set => specifications = value; }
        
        public int UnlockCost { get => unlockCost; set => unlockCost = value; }
        public int UnlockLevel { get => unlockLevel; set => unlockLevel = value; }
    }
}