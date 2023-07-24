using System;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Item.Products
{
    [Serializable]
    public class SpecObject
    {
        public SpecType Type;
        public int Value;

        public SpecObject() { }
        
        public SpecObject(SpecType type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}