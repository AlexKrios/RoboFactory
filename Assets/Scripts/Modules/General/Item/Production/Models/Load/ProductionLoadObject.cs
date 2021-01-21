using System;

namespace Modules.General.Item.Production.Models.Load
{
    [Serializable]
    public class ProductionLoadObject
    {
        // ReSharper disable once InconsistentNaming
        public Guid id;
        public string key;
        public int star;
        public long timeEnd;
    }
}
