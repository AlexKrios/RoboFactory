using Modules.General.Audio;
using Modules.General.Audio.Models;
using TMPro;
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

        #region Variables

        private Button _buttonComponent;

        #endregion
        
        #region Components

        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private GameObject locked;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            _buttonComponent = GetComponent<Button>();
            _buttonComponent.onClick.AddListener(Click);
            
            if (!_buttonComponent.interactable)
                locked.SetActive(true);
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