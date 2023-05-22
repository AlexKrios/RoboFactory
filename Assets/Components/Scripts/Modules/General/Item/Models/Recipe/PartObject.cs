using System;
using Components.Scripts.Modules.General.Item.Models.Scriptable;

namespace Components.Scripts.Modules.General.Item.Models.Recipe
{
    [Serializable]
    public class PartObject
    {
        public ItemScriptable data;
        public int star;
        public int count;
    }
}
