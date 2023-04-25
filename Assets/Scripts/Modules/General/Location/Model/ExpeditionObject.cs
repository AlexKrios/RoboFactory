using System;
using System.Collections.Generic;

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
    }
}