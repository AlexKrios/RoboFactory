using System;
using UnityEngine;

namespace Modules.General.Item.Production.Models.Object
{
    [Serializable]
    public class ProductionObject
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public int Star { get; set; }

        public long TimeEnd { get; set; }
        public bool IsLoad { get; set; }

        public Coroutine Timer { get; set; }
    }
}
