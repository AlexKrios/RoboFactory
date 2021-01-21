using System;
using System.Collections.Generic;

namespace Modules.General.Unit.Models
{
    [Serializable]
    public class UnitsLoadObject
    {
        public int groupCount;
        public List<UnitLoadObject> units;
    }
    
    [Serializable]
    public class UnitLoadObject
    {
        public string key;
        public int experience;
        public int level;
        public bool isLocked;
        public List<string> outfit;
    }
}
