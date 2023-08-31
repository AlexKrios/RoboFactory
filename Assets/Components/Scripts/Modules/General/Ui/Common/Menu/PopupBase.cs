using DG.Tweening;
using RoboFactory.General.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Common
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupBase : MonoBehaviour
    {
        private const float FadeTime = 0.25f;
        
        [Inject] private readonly AudioService _audioService;
        [Inject] protected readonly IUiController UiController;

        [SerializeField] private Button _close;
        
        private CanvasGroup _canvasGroup;
        protected readonly CompositeDisposable Disposable = new();

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _close.OnClickAsObservable().Subscribe(_ => Close()).AddTo(Disposable);
        }

        protected virtual void OnDestroy()
        {
            Disposable.Dispose();
        }
        
        public virtual void Initialize()
        {
            PlayFadeIn();
        }

        private void PlayFadeIn()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1, FadeTime).SetEase(Ease.InCubic);
        }

        protected virtual void Release()
        {
            UiController.RemoveUi(this, gameObject);
        }
        
        public void Close()
        {
            _audioService.PlayAudio(AudioClipType.CloseClick);

            _canvasGroup.alpha = 1f;
            _canvasGroup.DOFade(0, FadeTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(Release);
        }
    }
}
