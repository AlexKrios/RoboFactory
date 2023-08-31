using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Raw.Convert;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    public class PartResultCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ConvertRawService _convertRawService;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _progress;

        private ConversionMenuView _menu;
        
        protected void Awake()
        {
            _menu = _uiController.FindUi<ConversionMenuView>();
        }

        public async void SetResultData(AssetReference iconRef, int star)
        {
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(iconRef);
            _level.text = star.ToString();
        }
        
        public void StartConvertAnimation()
        {
            _menu.Convert.SetInteractable(false);
            _progress.DOFillAmount(1, 1).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _menu.Convert.SetState();
                _progress.fillAmount = 0;

                _convertRawService.AddRaw();
            });
        }
    }
}
