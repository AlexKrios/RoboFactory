using Modules.General.Audio;
using Modules.General.Audio.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.General.Ui.Common.Menu
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public abstract class MenuButtonBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IAudioController _audioController;
        [Inject(Id = "UiCamera")] private readonly Transform _uiCamera;
        [Inject(Id = "MenuCamera")] private readonly Transform _menuCamera;
        [Inject(Id = "UiCanvas")] private readonly RectTransform _uiCanvas;
        [Inject(Id = "MenuCanvas")] private readonly RectTransform _menuCanvas;

        #endregion

        #region Variables

        private Button _buttonComponent;
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            _disposable = new CompositeDisposable();

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
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            
            _uiCamera.gameObject.SetActive(false);
            _menuCamera.gameObject.SetActive(true);
            
            _uiCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(true);
        }
    }
}
