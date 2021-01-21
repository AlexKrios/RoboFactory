using System;
using System.Collections.Generic;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Raw.Models.Load;
using Modules.General.Item.Raw.Models.Object;
using Modules.General.Item.Raw.Models.Type;

namespace Modules.General.Item.Raw
{
    public interface IRawController
    {
        Action OnRawSet { get; set; }
        
        RawObject GetRaw(string key);
        List<RawObject> GetAllRaw();
        List<RawObject> GetRawByType(RawType type);
        List<RawObject> GetMainRaw();

        void LoadRawData(List<RawLoadObject> rawData);

        void SetMaxRaw();
        void SetMinRaw();

        void AddRaw(PartObject part);
        void AddRaw(string key, int star, int count);

        bool CheckIfRawStoreFull(string key, int star);
    }
}
