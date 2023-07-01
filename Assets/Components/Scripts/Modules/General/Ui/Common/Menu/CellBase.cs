using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Common
{
    [RequireComponent(typeof(Button))]
    public abstract class CellBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly AudioManager _audioController;

        #endregion

        #region Components

        [SerializeField] protected Image background;
        [SerializeField] protected Image icon;
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

        protected virtual void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion
        
        protected virtual void Click()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
        }

        protected virtual async void SetIconSprite(AssetReference spriteRef, bool active = true)
        {
            var color = icon.color;
            icon.color = new Color(color.r, color.g, color.b, 0);
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(spriteRef);
            icon.DORestart();
            icon.DOFade(1f, 0.1f);
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
