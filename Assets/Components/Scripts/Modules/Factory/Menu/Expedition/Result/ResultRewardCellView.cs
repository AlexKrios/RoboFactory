using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ResultRewardCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        private CanvasGroup _canvasGroup;

        public async void SetData(PartObject part)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(part.Data.IconRef);
            _count.text = part.Count.ToString();
        }
    }
}