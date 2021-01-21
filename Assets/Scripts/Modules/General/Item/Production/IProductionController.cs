using System;
using System.Collections.Generic;
using Modules.General.Item.Production.Models.Load;
using Modules.General.Item.Production.Models.Object;
using Modules.General.Scriptable;

namespace Modules.General.Item.Production
{
    public interface IProductionController
    {
        Action OnProductionComplete { get; set; }
        int Level { get; set; }
        int CellCount { get; set; }

        void LoadStoreData(ProductionsLoadObject data);

        bool IsHaveFreeCell();
        
        void AddProduction(ProductionObject productionObject);
        List<ProductionObject> GetAllProduction();
        ProductionObject GetProduction(Guid id);
        void RemoveProduction(Guid id);
        
        UpgradeDataObject GetUpgradeData();

        bool IsEnoughParts(ProductionObject productionObj);
        void RemoveParts(ProductionObject productionObj);
    }
}
