using Modules.General.Item.Products.Models.Types;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Scriptable.Type
{
    [CreateAssetMenu(fileName = "EquipmentProductData", menuName = "Scriptable/General/Product/Equipment", order = 73)]
    public class EquipmentProductScriptable : ProductScriptable
    {
        public EquipmentProductScriptable()
        {
            ProductGroup = ProductGroup.Equipment;
        }
    }
}