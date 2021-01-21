using System;
using Modules.General.Item.Models;
using Modules.General.Item.Raw.Models.Scriptable;
using Modules.General.Item.Raw.Models.Type;

namespace Modules.General.Item.Raw.Models.Object
{
    [Serializable]
    public class RawObject : ItemBase
    {
        public RawType RawType { get; set; }

        public int Level { get; set; }
        
        public bool IsRefill { get; set; }

        public RawSettingsObject Settings { get; set; }
    }
}
