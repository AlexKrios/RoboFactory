using Components.Scripts.Modules.General.Item.Products.Types;
using UnityEngine;

namespace Components.Scripts.Modules.General.Item.Products.Scriptable.Type
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