using Modules.General.Audio;
using Modules.General.Audio.Models;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.General.Ui.Common.Menu
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public abstract class StarButtonBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IAudioController _audioController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text level;
        [SerializeField] private GameObject locked;

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

            if (!_buttonComponent.interactable)
                locked.SetActive(true);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion

        protected virtual void Click()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
        }

        protected void SetStarLevel(int levelValue)
        {
            level.text = levelValue.ToString();
        }
    }
}