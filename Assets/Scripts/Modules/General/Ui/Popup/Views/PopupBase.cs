using DG.Tweening;
using Modules.General.Audio;
using Modules.General.Audio.Models;
using UnityEngine;
using Zenject;

namespace Modules.General.Ui.Popup.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupBase : MonoBehaviour
    {
        private const float FadeInTime = 0.25f;
        private const float FadeOutTime = 0.1f;
        
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly IUiController _uiController;
        
        [SerializeField] protected UiType type;

        private CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }
        
        protected void Close()
        {
            _audioController.PlayAudio(AudioClipType.CloseClick);
            _canvasGroup.DOFade(0, FadeOutTime).SetEase(Ease.OutCubic)
                .OnComplete(() => _uiController.RemoveUi(type));
        }

        protected void PlayFadeIn()
        {
            _canvasGroup.DOFade(1, FadeInTime).SetEase(Ease.OutCubic);
        }

        protected void PlayFadeInOut(float delay)
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(_canvasGroup.DOFade(1, FadeInTime).SetEase(Ease.OutCubic))
                .AppendInterval(delay)
                .Append(_canvasGroup.DOFade(0, FadeOutTime).SetEase(Ease.OutCubic))
                .OnComplete(() => _uiController.RemoveUi(type));
        }
    }
}
