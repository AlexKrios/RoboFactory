using System;
using System.Collections.Generic;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Products.Models.Types;
using UnityEngine;

namespace Modules.General.Order.Models.Scriptable
{
    [CreateAssetMenu(fileName = "OrderData", menuName = "Scriptable/General/Order/Data", order = 91)]
    public class OrderScriptable : ScriptableObject
    {
        [SerializeField] private List<OrderData> orders;
        
        public List<OrderData> Orders => orders;
    }
    
    [Serializable]
    public class OrderData
    {
        [SerializeField] private string key;
        [SerializeField] private ProductGroup group;
        [SerializeField] private PartObject part;

        public string Key => key;
        public ProductGroup Group => group;
        public PartObject Part => part;
    }
}