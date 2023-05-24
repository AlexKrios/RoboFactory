using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Ui.Popup
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupBase : MonoBehaviour
    {
        private const float FadeInTime = 0.25f;
        private const float FadeOutTime = 0.1f;

        #region Zenject
        
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] protected UiType type;

        #endregion

        #region Varaibles

        private CanvasGroup _canvasGroup;
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            _disposable = new CompositeDisposable();
            _canvasGroup = GetComponent<CanvasGroup>();

            PlayFadeIn();
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion
        
        private void PlayFadeIn()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, FadeInTime).SetEase(Ease.OutCubic);
        }

        protected void Close(float delay)
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(_canvasGroup.DOFade(1, FadeInTime).SetEase(Ease.OutCubic))
                .AppendInterval(delay)
                .Append(_canvasGroup.DOFade(0, FadeOutTime).SetEase(Ease.OutCubic))
                .OnComplete(() => _uiController.RemoveUi(this, gameObject));
        }
    }
}
