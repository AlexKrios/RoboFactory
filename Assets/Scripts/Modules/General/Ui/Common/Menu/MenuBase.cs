﻿using DG.Tweening;
using Modules.General.Audio;
using Modules.General.Audio.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.General.Ui.Common.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class MenuBase : MonoBehaviour
    {
        private const float FadeTime = 0.25f;
        
        #region Zenject
        
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components
        
        [SerializeField] private UiType type;
        
        [Space]
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
            
            _uiController.AddUi(type, gameObject);

            PlayFadeIn();
        }

        private void OnDestroy()
        {
            Disposable.Dispose();
        }

        #endregion

        private void PlayFadeIn()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1, FadeTime).SetEase(Ease.InCubic);
        }
        
        public virtual void Close()
        {
            _audioController.PlayAudio(AudioClipType.CloseClick);
            
            _canvasGroup.alpha = 1f;
            _canvasGroup.DOFade(0, FadeTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => _uiController.RemoveUi(type));
        }
    }
}
