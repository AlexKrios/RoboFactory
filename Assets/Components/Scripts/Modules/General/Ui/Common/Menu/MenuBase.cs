using Components.Scripts.Modules.General.Audio;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.General.Ui.Common.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class MenuBase : MonoBehaviour
    {
        private const float FadeTime = 0.25f;
        
        #region Zenject
        
        [Inject] private readonly AudioManager _audioController;
        [Inject] protected readonly IUiController UiController;

        #endregion

        #region Components
        
        [SerializeField] private Button close;

        #endregion
        
        #region Variables

        private CanvasGroup _canvasGroup;
        protected CompositeDisposable Disposable;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            Disposable = new CompositeDisposable();
            _canvasGroup = GetComponent<CanvasGroup>();
            close.OnClickAsObservable().Subscribe(_ => Close()).AddTo(Disposable);

            PlayFadeIn();
        }

        protected virtual void OnDestroy()
        {
            Disposable.Dispose();
        }

        #endregion

        private void PlayFadeIn()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1, FadeTime).SetEase(Ease.InCubic);
            UiController.SetCameraActive(CameraType.Main, false);
            UiController.SetCanvasActive(CanvasType.HUD, false);
        }
        
        public virtual void Close()
        {
            _audioController.PlayAudio(AudioClipType.CloseClick);
            UiController.SetCameraActive(CameraType.Main);
            UiController.SetCanvasActive(CanvasType.HUD);
            
            _canvasGroup.alpha = 1f;
            _canvasGroup.DOFade(0, FadeTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => UiController.RemoveUi(this, gameObject));
        }
    }
}
