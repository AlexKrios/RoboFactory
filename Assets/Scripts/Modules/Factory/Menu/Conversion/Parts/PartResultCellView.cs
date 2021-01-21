using DG.Tweening;
using Modules.General.Asset;
using Modules.General.Item.Raw.Convert;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Conversion.Parts
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Part Result Cell View")]
    public class PartResultCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ConversionMenuManager _conversionMenuManager;
        [Inject] private readonly IConvertRawController _convertRawController;

        #endregion

        #region Components
        
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private Image progress;

        #endregion

        public async void SetResultData(AssetReference iconRef, int star)
        {
            icon.sprite = await AssetsController.LoadAsset<Sprite>(iconRef);
            level.text = star.ToString();
        }
        
        public void StartConvertAnimation()
        {
            _conversionMenuManager.Convert.SetInteractable(false);
            progress.DOFillAmount(1, 1).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _conversionMenuManager.Convert.SetState();
                progress.fillAmount = 0;

                _convertRawController.AddRaw();
            });
        }
    }
}
