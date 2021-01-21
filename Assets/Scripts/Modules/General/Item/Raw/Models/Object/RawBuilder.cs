using Modules.General.Item.Raw.Models.Scriptable;

namespace Modules.General.Item.Raw.Models.Object
{
    public class RawBuilder
    {
        public RawObject Create(RawScriptable data)
        {
            return new RawObject
            {
                Key = data.Key,

                ItemType = data.ItemType,
                RawType = data.RawType,
                
                IconRef = data.IconRef,
                
                Count = 0,
                Level = 1,
                
                IsRefill = data.IsMain,
                
                //Recipe = data.Recipes
            };
        }
    }
}
