using Modules.Factory.Menu.Settings.Language;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Settings
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Settings Menu View")]
    public class SettingsMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;

        #endregion

        #region Components

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private LanguageSectionView language;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            language.OnClickEvent += OnLanguageClick;
            
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.SettingsMenuTitleKey);
        }

        #endregion

        private void OnLanguageClick()
        {
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.SettingsMenuTitleKey);
        }
    }
}
