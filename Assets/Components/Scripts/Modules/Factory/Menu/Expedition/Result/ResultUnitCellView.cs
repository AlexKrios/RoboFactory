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

        [Inject] private readonly AddressableService addressableService;

        #endregion

        #region Components

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _level;

        #endregion

        #region Variables

        private CanvasGroup _canvasGroup;

        #endregion

        public async void SetData(UnitObject unit)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(unit.IconRef);
            _level.text = unit.Level.ToString();
        }
    }
}