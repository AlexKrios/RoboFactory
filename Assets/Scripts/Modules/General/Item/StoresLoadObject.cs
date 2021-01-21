using System;
using System.Collections.Generic;
using Modules.General.Item.Products.Models.Load;
using Modules.General.Item.Raw.Models.Load;

namespace Modules.General.Item
{
    [Serializable]
    public class StoresLoadObject
    {
        public List<RawLoadObject> raw;
        public List<ProductLoadObject> products;
    }
}
