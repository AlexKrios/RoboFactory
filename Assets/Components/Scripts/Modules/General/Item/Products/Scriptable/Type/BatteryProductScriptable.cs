using Components.Scripts.Modules.General.Item.Products.Types;
using UnityEngine;

namespace Components.Scripts.Modules.General.Item.Products.Scriptable.Type
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