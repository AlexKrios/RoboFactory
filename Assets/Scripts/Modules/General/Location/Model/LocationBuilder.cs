namespace Modules.General.Location.Model
{
    public class LocationBuilder
    {
        public LocationObject Create(LocationScriptable data)
        {
            return new LocationObject
            {
                Key = data.Key,

                Time = data.Time,

                IconRef = data.IconRef,
                
                Enemies = data.Enemies,
                Reward = data.Reward
            };
        }
    }
}
