using Components.Scripts.Modules.Authentication;
using Components.Scripts.Modules.Factory.Menu.Settings.Language;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Scene;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Settings
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Settings Menu View")]
    public class SettingsMenuView : PopupBase
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly AuthenticationManager _authenticationManager;
        [Inject] private readonly SceneController _sceneController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
        [SerializeField] private Button signOut;
        [SerializeField] private LanguageSectionView language;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            language.OnClickEvent += OnLanguageClick;
            
            if (!_authenticationManager.IsGooglePlayConnected())
                signOut.onClick.AddListener(OnSignOutClick);
            else
                signOut.gameObject.SetActive(false);
            
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.SettingsMenuTitleKey);
        }

        #endregion

        private void OnSignOutClick()
        {
            _authenticationManager.SignOut();
            _sceneController.LoadScene(SceneName.Authentication);
        }
        
        private void OnLanguageClick()
        {
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.SettingsMenuTitleKey);
        }
    }
}
