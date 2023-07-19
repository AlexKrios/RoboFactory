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
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Result Reward Cell View")]
    public class ResultRewardCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AddressableService addressableService;

        #endregion
        
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        private CanvasGroup _canvasGroup;

        public async void SetData(PartObject part)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(part.data.IconRef);
            _count.text = part.count.ToString();
        }
    }
}