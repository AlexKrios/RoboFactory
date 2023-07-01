using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Result Unit Cell View")]
    public class ResultUnitCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AssetsManager _assetsManager;

        #endregion
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text level;
        
        private CanvasGroup _canvasGroup;

        public async void SetData(UnitObject unit)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(unit.IconRef);
            level.text = unit.Level.ToString();
        }
    }
}