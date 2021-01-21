using Modules.General.Audio;
using Modules.General.Audio.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.General.Ui.Common
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public abstract class ButtonBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IAudioController _audioController;

        #endregion

        #region Compnents

        [SerializeField] private TextMeshProUGUI buttonText;

        #endregion
        
        #region Variables

        private Button _button;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        #endregion

        protected void SetButtonText(string text)
        {
            buttonText.text = text;
        }
        
        protected virtual void Click()
        {
            _audioController.PlayAudio(AudioClipType.ButtonClick);
        }

        public void SetInteractable(bool value)
        {
            _button.interactable = value;
        }

        public virtual void SetState() { }
    }
}
