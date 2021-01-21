using DG.Tweening;
using Modules.General.Asset;
using Modules.General.Item.Models.Recipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Factory.Menu.Expedition.Result
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Result Reward Cell View")]
    public class ResultRewardCellView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI count;

        private CanvasGroup _canvasGroup;

        public async void SetData(PartObject part)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            
            icon.sprite = await AssetsController.LoadAsset<Sprite>(part.data.IconRef);
            count.text = part.count.ToString();
        }
    }
}