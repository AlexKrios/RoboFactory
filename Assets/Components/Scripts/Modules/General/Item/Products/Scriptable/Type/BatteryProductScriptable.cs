using UnityEngine;

namespace RoboFactory.General.Item.Products
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