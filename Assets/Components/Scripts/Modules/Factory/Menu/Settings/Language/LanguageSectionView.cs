using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Localization;
using RoboFactory.General.Settings;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    public class LanguageSectionView : MonoBehaviour
    {
        [Inject] private readonly SettingsService _settingsController;
        [Inject] private readonly LocalizationService _localizationService;
        
        [SerializeField] private List<LanguageCellView> _languages;

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
            _localizationService.LoadLocalizationData();

            OnClickEvent?.Invoke();
        }
    }
}