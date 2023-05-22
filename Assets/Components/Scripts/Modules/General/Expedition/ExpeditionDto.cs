using System;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Expedition
{
    [Serializable]
    public class ExpeditionDto
    {
        [JsonProperty("id")]
        public Guid Id;
        public string key;
        public string star;
        public long timeEnd;
    }
}