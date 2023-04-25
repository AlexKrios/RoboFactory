using Modules.General.Item.Products.Models.Types;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Scriptable.Type
{
    [CreateAssetMenu(fileName = "EngineProductData", menuName = "Scriptable/General/Product/Engine", order = 73)]
    public class EngineProductScriptable : ProductScriptable
    {
        public EngineProductScriptable()
        {
            ProductGroup = ProductGroup.Engine;
        }
    }
}