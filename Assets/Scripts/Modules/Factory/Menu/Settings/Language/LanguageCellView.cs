using System;
using Modules.General.Audio;
using Modules.General.Audio.Models;
using Modules.General.Settings.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Settings.Language
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Language Cell View")]
    public class LanguageCellView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IAudioController _audioController;

        #endregion
        
        #region Components

        [SerializeField] private LanguageType type;

        [Space]
        [SerializeField] private Image active;

        public LanguageType Type => type;

        #endregion

        #region Variables

        public Action<LanguageCellView, LanguageType> OnClickEvent { get; set; }

        private Button _button;
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            _button = GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion

        public void Click()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            
            OnClickEvent?.Invoke(this, type);
        }
        
        public void SetActive(bool value)
        {
            active.gameObject.SetActive(value);
        }
    }
}