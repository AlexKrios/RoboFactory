using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.General.Location.Model
{
    public class ExpeditionObject
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Star { get; set; }
        public List<string> Units { get; set; }
        public List<string> Enemy { get; set; }
        
        public long TimeEnd { get; set; }

        public Coroutine Timer { get; set; }
    }
}