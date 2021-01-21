using Modules.General.Audio;
using Modules.General.Audio.Models;
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

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            _buttonComponent = GetComponent<Button>();
            _buttonComponent.onClick.AddListener(Click);
            
            if (!_buttonComponent.interactable)
                locked.SetActive(true);
        }

        private void Update()
        {
            if (activeImage != null && activeImage.gameObject.activeSelf)
                activeImage.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
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
        
        protected void SetAlpha(bool state)
        {
            var backgroundCanvasGroup = background.GetComponent<CanvasGroup>();
            backgroundCanvasGroup.alpha = state ? 1f : 0.25f;
        }
    }
}
