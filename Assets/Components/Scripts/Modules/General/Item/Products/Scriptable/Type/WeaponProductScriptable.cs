using Components.Scripts.Modules.General.Item.Products.Types;
using UnityEngine;

namespace Components.Scripts.Modules.General.Item.Products.Scriptable.Type
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