using DG.Tweening;
using Modules.General.Audio;
using Modules.General.Audio.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Modules.General.Ui.Popup.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupBase : MonoBehaviour
    {
        private const float FadeInTime = 0.25f;
        private const float FadeOutTime = 0.1f;

        #region Zenject

        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] protected UiType type;

        #endregion

        #region Varaibles

        private CanvasGroup _canvasGroup;
        protected CompositeDisposable Disposable;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            Disposable = new CompositeDisposable();
            
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }
        
        private void OnDestroy()
        {
            Disposable.Dispose();
        }

        #endregion

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

        protected void Close(float delay)
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
