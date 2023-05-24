using UnityEngine;

namespace RoboFactory.General.Item.Products
{
    [CreateAssetMenu(fileName = "ArmorProductData", menuName = "Scriptable/General/Product/Armor", order = 72)]
    public class ArmorProductScriptable : ProductScriptable
    {
        public ArmorProductScriptable()
        {
            ProductGroup = ProductGroup.Armor;
        }
    }
}