using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Localisation;
using RoboFactory.General.Settings;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Language Section View")]
    public class LanguageSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly SettingsService _settingsController;
        [Inject] private readonly LocalizationService localizationController;

        #endregion
        
        #region Components

        [SerializeField] private List<LanguageCellView> _languages;

        #endregion

        #region Variables

        public Action OnClickEvent { get; set; }
        
        private LanguageCellView _activeLanguage;
        private LanguageCellView ActiveLanguage
        {
            get => _activeLanguage;
            set
            {
                if (_activeLanguage != null)
                    _activeLanguage.SetActive(false);

                _activeLanguage = value;
                _activeLanguage.SetActive(true);
            }
        }

        #endregion

        public void SetData()
        {
            _languages.ForEach(x => x.OnClickEvent += OnLanguageClick);

            ActiveLanguage = _languages.First(x => x.Type == _settingsController.Language);
        }

        private void OnLanguageClick(LanguageCellView cell, LanguageType type)
        {
            if (ActiveLanguage == cell)
                return;

            _settingsController.SetLanguage(type);
            ActiveLanguage = cell;
            localizationController.LoadLocalisationData();

            OnClickEvent?.Invoke();
        }
    }
}