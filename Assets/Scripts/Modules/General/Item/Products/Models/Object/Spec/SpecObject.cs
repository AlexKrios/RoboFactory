using System;
using Modules.General.Item.Products.Models.Types;

namespace Modules.General.Item.Products.Models.Object.Spec
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