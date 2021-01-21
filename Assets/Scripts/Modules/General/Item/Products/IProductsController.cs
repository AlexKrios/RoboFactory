using System.Collections.Generic;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Products.Models.Load;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Type;

namespace Modules.General.Item.Products
{
    public interface IProductsController
    {
        void LoadProductsData(List<ProductLoadObject> products);
        ProductObject GetProduct(string key);
        List<ProductObject> GetAllProducts();
        ProductObject GetDefaultProduct(ProductGroup group, UnitType unit);

        bool IsEnoughProduct(List<PartObject> parts);
    }
}
