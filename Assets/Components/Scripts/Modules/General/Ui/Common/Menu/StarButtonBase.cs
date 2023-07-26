using RoboFactory.General.Audio;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Common
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public abstract class StarButtonBase : MonoBehaviour
    {
        [Inject] private readonly AudioService _audioService;

        [SerializeField] private TMP_Text _level;
        [SerializeField] private GameObject _locked;

        private Button _buttonComponent;
        private readonly CompositeDisposable _disposable = new();
        
        protected virtual void Awake()
        {
            _buttonComponent = GetComponent<Button>();
            _buttonComponent.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);

            if (!_buttonComponent.interactable)
                _locked.SetActive(true);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        protected virtual void Click()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
        }

        protected void SetStarLevel(int levelValue)
        {
            _level.text = levelValue.ToString();
        }
    }
}