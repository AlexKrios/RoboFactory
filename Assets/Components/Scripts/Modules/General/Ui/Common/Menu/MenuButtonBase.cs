using RoboFactory.General.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Common
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public abstract class MenuButtonBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AudioService _audioService;

        #endregion

        #region Variables

        private Button _buttonComponent;
        private readonly CompositeDisposable _disposable = new();

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            _buttonComponent = GetComponent<Button>();
            _buttonComponent.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion

        protected virtual void Click()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
        }
    }
}
