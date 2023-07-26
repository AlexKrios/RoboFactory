using RoboFactory.General.Audio;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Common
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public abstract class ButtonBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AudioService _audioService;

        #endregion

        #region Compnents

        [SerializeField] private TMP_Text _buttonText;

        #endregion
        
        #region Variables

        private Button _button;
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
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

        protected void SetButtonText(string text)
        {
            _buttonText.text = text;
        }
        
        protected virtual void Click()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
        }

        public void SetInteractable(bool value)
        {
            _button.interactable = value;
        }

        public virtual void SetState() { }
    }
}
