using System;
using System.Collections.Generic;
using RoboFactory.General.Item;
using RoboFactory.General.Item.Products;
using UnityEngine;

namespace RoboFactory.General.Order
{
    [CreateAssetMenu(fileName = "OrderData", menuName = "Scriptable/General/Order/Data", order = 91)]
    public class OrderScriptable : ScriptableObject
    {
        [SerializeField] private List<OrderData> _orders;
        
        public List<OrderData> Orders => _orders;
    }
    
    [Serializable]
    public class OrderData
    {
        [SerializeField] private string _key;
        [SerializeField] private ProductGroup _group;
        [SerializeField] private PartObject _part;

        public string Key => _key;
        public ProductGroup Group => _group;
        public PartObject Part => _part;
    }
}