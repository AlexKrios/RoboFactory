using Components.Scripts.Modules.General.Item.Products.Types;
using UnityEngine;

namespace Components.Scripts.Modules.General.Item.Products.Scriptable.Type
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