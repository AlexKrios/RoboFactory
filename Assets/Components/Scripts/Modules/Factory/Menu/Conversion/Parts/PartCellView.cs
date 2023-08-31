using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    public class PartCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly ManagersResolver _managersResolver;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _level;

        public async void SetPartData(PartObject part)
        {
            var data = part.Data;
            var store = _managersResolver.GetManagerByType(data.ItemType);
            var itemCount = store.GetItem(data.Key).Count;
            var sprite = await _addressableService.LoadAssetAsync<Sprite>(data.IconRef);

            _icon.sprite = sprite;
            _count.text = $"{itemCount}/{part.Count}";
            _level.text = part.Star.ToString();
        }
    }
}
