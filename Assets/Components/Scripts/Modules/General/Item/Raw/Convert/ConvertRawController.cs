using System.Linq;
using Components.Scripts.Modules.General.Item.Models.Types;
using JetBrains.Annotations;
using Zenject;

namespace Components.Scripts.Modules.General.Item.Raw.Convert
{
    [UsedImplicitly]
    public class ConvertRawController
    {
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly ManagersResolver managersResolver;

        private string _key;

        public bool IsEnoughRaw(string key)
        {
            _key = key;

            var store = managersResolver.GetManagerByType(ItemType.Raw);
            return store.GetItem(key).Recipe.Parts
                .Select(partObj => store.GetItem(partObj.data.Key).IsEnoughCount(partObj))
                .All(isEnough => isEnough);
        }

        public async void RemoveParts()
        {
            var store = managersResolver.GetManagerByType(ItemType.Raw);
            var recipe = store.GetItem(_key).Recipe;
            foreach (var part in recipe.Parts)
            {
                //TODO В один запрос сделать
                await _rawManager.RemoveItem(part.data.Key, part.count);
            }
        }
        
        public async void AddRaw()
        {
            await _rawManager.AddItem(_key);
        }
    }
}
