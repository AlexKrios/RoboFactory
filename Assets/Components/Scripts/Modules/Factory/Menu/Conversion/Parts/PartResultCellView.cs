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
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Part Result Cell View")]
    public class PartResultCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ConvertRawController _convertRawController;

        #endregion

        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text level;
        [SerializeField] private Image progress;

        #endregion

        #region Variables

        private ConversionMenuView _menu;

        #endregion
        
        #region Unity Methods

        protected void Awake()
        {
            _menu = _uiController.FindUi<ConversionMenuView>();
        }

        #endregion

        public async void SetResultData(AssetReference iconRef, int star)
        {
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(iconRef);
            level.text = star.ToString();
        }
        
        public void StartConvertAnimation()
        {
            _menu.Convert.SetInteractable(false);
            progress.DOFillAmount(1, 1).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _menu.Convert.SetState();
                progress.fillAmount = 0;

                _convertRawController.AddRaw();
            });
        }
    }
}
