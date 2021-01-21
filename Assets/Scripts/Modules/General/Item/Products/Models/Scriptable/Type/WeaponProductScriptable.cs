using Modules.General.Item.Products.Models.Types;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Scriptable.Type
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