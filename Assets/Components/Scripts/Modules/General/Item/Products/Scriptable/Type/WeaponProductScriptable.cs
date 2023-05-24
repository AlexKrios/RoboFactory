using UnityEngine;

namespace RoboFactory.General.Item.Products
{
    [CreateAssetMenu(fileName = "WeaponProductData", menuName = "Scriptable/General/Product/Weapon", order = 71)]
    public class WeaponProductScriptable : ProductScriptable
    {
        public WeaponProductScriptable()
        {
            ProductGroup = ProductGroup.Weapon;
        }
    }
}