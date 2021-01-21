using Modules.General.Item.Products.Models.Types;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Scriptable.Type
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