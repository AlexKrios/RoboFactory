using System;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Products.Models.Types;

namespace Modules.General.Order.Models.Object
{
    [Serializable]
    public class OrderObject
    {
        public string key;
        public ProductGroup group;
        public PartObject part;

        public bool isActive;
        public bool isComplete;
    }
}