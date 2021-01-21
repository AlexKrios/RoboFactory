using System;
using System.Collections.Generic;

namespace Modules.General.Item.Production.Models.Load
{
    [Serializable]
    public class ProductionsLoadObject
    {
        public int level;
        public int count;
        public List<ProductionLoadObject> production;
    }
}
