using System.Linq;
using JetBrains.Annotations;
using RoboFactory.General.Services;
using Zenject;

namespace RoboFactory.General.Item.Raw.Convert
{
    [UsedImplicitly]
    public class ConvertRawService : Service
    {
        [Inject] private readonly RawService _rawService;
        [Inject] private readonly ManagersResolver _managersResolver;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;

        private string _key;

        public bool IsEnoughRaw(string key)
        {
            _key = key;

            var store = _managersResolver.GetManagerByType(ItemType.Raw);
            return store.GetItem(key).Recipe.Parts
                .Select(partObj => store.GetItem(partObj.Data.Key).IsEnoughCount(partObj))
                .All(isEnough => isEnough);
        }

        public async void RemoveParts()
        {
            var store = _managersResolver.GetManagerByType(ItemType.Raw);
            var recipe = store.GetItem(_key).Recipe;
            foreach (var part in recipe.Parts)
            {
                //TODO В один запрос сделать
                await _rawService.RemoveItem(part.Data.Key, part.Count);
            }
        }
        
        public async void AddRaw()
        {
            await _rawService.AddItem(_key);
        }
    }
}
