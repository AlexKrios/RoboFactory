using UnityEngine;

namespace RoboFactory.General.Item.Products
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