using System;
using RoboFactory.General.Audio;
using RoboFactory.General.Settings;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Language Cell View")]
    public class LanguageCellView : MonoBehaviour
    {
        [Inject] private readonly AudioManager _audioManager;
        
        [SerializeField] private LanguageType _type;

        [Space]
        [SerializeField] private Image _active;

        public LanguageType Type => _type;

        public Action<LanguageCellView, LanguageType> OnClickEvent { get; set; }

        private Button _button;
        private readonly CompositeDisposable _disposable = new();
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        public void Click()
        {
            _audioManager.PlayAudio(AudioClipType.ButtonClick);
            
            OnClickEvent?.Invoke(this, _type);
        }
        
        public void SetActive(bool value)
        {
            _active.gameObject.SetActive(value);
        }
    }
}