using Components.Scripts.Modules.General.Item.Products.Scriptable;

namespace Components.Scripts.Modules.General.Item.Products.Object
{
    public class ProductBuilder
    {
        private readonly ProductScriptable _data;

        public ProductBuilder(ProductScriptable data)
        {
            _data = data;
        }

        public TData Create<TData>() where TData : ProductObject, new()
        {
            return new TData
            {
                Key = _data.Key,
                ItemType = _data.ItemType,
                UnitType = _data.UnitType,
                ProductGroup = _data.ProductGroup,
                ProductType = _data.ProductType,
                IsProduct = _data.IsProduct,

                IconRef = _data.IconRef,
                Model = _data.Model,

                Index = _data.Index,
                Count = 0,
                Experience = 0,
                
                Level = 1,

                Recipe = _data.Recipe
            };
        }
    }
}