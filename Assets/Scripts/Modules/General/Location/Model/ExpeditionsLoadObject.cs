using System;
using System.Collections.Generic;

namespace Modules.General.Location.Model
{
    [Serializable]
    public class ExpeditionsLoadObject
    {
        public int count;
        public List<ExpeditionLoadObject> expeditions;
    }
}
