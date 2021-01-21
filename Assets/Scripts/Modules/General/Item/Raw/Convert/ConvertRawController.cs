using System.Linq;
using Modules.General.Item.Models.Types;
using Zenject;

namespace Modules.General.Item.Raw.Convert
{
    public class ConvertRawController : IConvertRawController
    {
        [Inject] private readonly IRawController _rawController;
        [Inject] private readonly ControllersResolver _controllersResolver;

        private string _key;
        private int _star;
        
        public bool IsEnoughRaw(string key, int star)
        {
            _key = key;
            _star = star;

            var store = _controllersResolver.GetStoreByType(ItemType.Raw);
            return store.GetItem(key).Recipe.Parts
                .Select(partObj => store.GetItem(partObj.data.Key).IsEnoughCount(partObj))
                .All(isEnough => isEnough);
        }

        public void RemoveParts()
        {
            var store = _controllersResolver.GetStoreByType(ItemType.Raw);
            var recipe = store.GetItem(_key).Recipe;
            foreach (var part in recipe.Parts)
            {
                _rawController.AddRaw(part.data.Key, part.star, -part.count);
            }
        }
        
        public void AddRaw()
        {
            _rawController.AddRaw(_key, _star, 1);
        }
    }
}
