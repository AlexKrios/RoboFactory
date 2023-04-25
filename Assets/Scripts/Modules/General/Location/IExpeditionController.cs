using System;
using System.Collections.Generic;
using Modules.General.Location.Model;
using Modules.General.Scriptable;

namespace Modules.General.Location
{
    public interface IExpeditionController
    {
        Action OnExpeditionComplete { get; set; }
        
        int CellCount { get; set; }
        LocationObject CurrentBattleLocation { get; set; }
        
        void LoadStoreData(ExpeditionsLoadObject data);
        bool IsHaveFreeCell();
        
        List<LocationObject> GetLocations();
        LocationObject GetLocation(string key);

        void AddExpedition(ExpeditionObject data);
        List<ExpeditionObject> GetAllExpeditions();
        ExpeditionObject GetExpedition(Guid id);
        UpgradeDataObject GetUpgradeData();
        void RemoveExpedition(Guid id);
    }
}
