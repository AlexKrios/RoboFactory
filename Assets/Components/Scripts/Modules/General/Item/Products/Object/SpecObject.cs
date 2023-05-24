using System;

namespace RoboFactory.General.Item.Products
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