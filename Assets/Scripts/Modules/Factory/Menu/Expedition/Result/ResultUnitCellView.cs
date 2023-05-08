using DG.Tweening;
using Modules.General.Asset;
using Modules.General.Unit.Object;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Factory.Menu.Expedition.Result
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Result Unit Cell View")]
    public class ResultUnitCellView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text level;
        
        private CanvasGroup _canvasGroup;

        public async void SetData(UnitObject unit)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            
            icon.sprite = await AssetsController.LoadAsset<Sprite>(unit.IconRef);
            level.text = unit.Level.ToString();
        }
    }
}