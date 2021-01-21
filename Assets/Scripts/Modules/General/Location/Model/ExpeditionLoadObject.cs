using System;
using System.Collections.Generic;

namespace Modules.General.Location.Model
{
    [Serializable]
    public class ExpeditionLoadObject
    {
        // ReSharper disable once InconsistentNaming
        public Guid id;
        public string key;
        public string star;
        public List<string> units;
        
        public long timeEnd;
    }
}
