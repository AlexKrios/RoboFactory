using Modules.General.Audio;
using Modules.General.Audio.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.General.Ui.Common.Menu
{
    [RequireComponent(typeof(Button))]
    public abstract class CellBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IAudioController _audioController;

        #endregion

        #region Components

        [SerializeField] protected Image background;
        [SerializeField] private Image icon;
        [SerializeField] private Image activeImage;
        
        [Space]
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

        private void Update()
        {
            if (activeImage != null && activeImage.gameObject.activeSelf)
                activeImage.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
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

        protected virtual void SetIconSprite(Sprite sprite, bool active = true)
        {
            icon.sprite = sprite;
            icon.gameObject.SetActive(active);
        }

        public void SetInactive()
        {
            activeImage.gameObject.SetActive(false);
            activeImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        public void SetActive()
        {
            activeImage.gameObject.SetActive(true);
        }
    }
}
