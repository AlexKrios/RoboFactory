using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoboFactory.Factory.Menu.Expedition
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Result Reward Cell View")]
    public class ResultRewardCellView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text count;

        private CanvasGroup _canvasGroup;

        public async void SetData(PartObject part)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);
            
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(part.data.IconRef);
            count.text = part.count.ToString();
        }
    }
}