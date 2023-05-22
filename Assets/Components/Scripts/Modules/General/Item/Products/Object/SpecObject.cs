using System;
using Components.Scripts.Modules.General.Item.Products.Types;

namespace Components.Scripts.Modules.General.Item.Products.Object
{
    [Serializable]
    public class SpecObject
    {
        public SpecType type;
        public int value;

        public SpecObject() { }
        
        public SpecObject(SpecType type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }
}