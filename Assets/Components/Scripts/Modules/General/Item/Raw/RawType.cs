using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Item.Raw
{
    public enum RawType
    {
        None = -1,
        All = 0,
        
        [JsonProperty("weapon")]
        Weapon = 1,
        
        [JsonProperty("armor")]
        Armor = 2,
        
        [JsonProperty("engine")]
        Engine = 3,
        
        [JsonProperty("battery")]
        Battery = 4,
    }
}
