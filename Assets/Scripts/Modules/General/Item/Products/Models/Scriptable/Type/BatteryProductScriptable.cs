using Modules.General.Item.Products.Models.Types;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Scriptable.Type
{
    [CreateAssetMenu(fileName = "BatteryProductData", menuName = "Scriptable/General/Product/Battery", order = 74)]
    public class BatteryProductScriptable : ProductScriptable
    {
        public BatteryProductScriptable()
        {
            ProductGroup = ProductGroup.Battery;
        }
    }
}