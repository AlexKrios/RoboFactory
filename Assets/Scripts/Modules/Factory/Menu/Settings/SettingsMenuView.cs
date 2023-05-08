using Modules.Authentication;
using Modules.Factory.Menu.Settings.Language;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Scene;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Settings
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Settings Menu View")]
    public class SettingsMenuView : PopupBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
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
