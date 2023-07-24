using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Common
{
    public class SpecCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;

        [SerializeField] private SpecType _specType;
        
        [Space]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        public SpecType SpecType => _specType;

        public async void SetData(SpecObject spec)
        {
            var iconRef = _addressableService.Assets.GetSpecIcon(spec.Type);
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(iconRef);
            _count.text = spec.Value.ToString();
        }
    }
}
