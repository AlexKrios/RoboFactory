using System;
using Modules.General.Item.Models.Scriptable;

namespace Modules.General.Item.Models.Recipe
{
    [Serializable]
    public class PartObject
    {
        public ItemScriptable data;
        public int star;
        public int count;
    }
}
