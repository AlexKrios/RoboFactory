using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Settings;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Settings.Language
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Language Section View")]
    public class LanguageSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly SettingsManager _settingsController;
        [Inject] private readonly LocalisationManager _localisationController;

        #endregion
        
        #region Components

        [SerializeField] private List<LanguageCellView> languages;

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

        #region Unity Methods

        private void Awake()
        {
            SetData();
        }
        
        #endregion

        private void SetData()
        {
            languages.ForEach(x => x.OnClickEvent += OnLanguageClick);

            ActiveLanguage = languages.First(x => x.Type == _settingsController.Language);
        }

        private void OnLanguageClick(LanguageCellView cell, LanguageType type)
        {
            if (ActiveLanguage == cell)
                return;

            _settingsController.SetLanguage(type);

            ActiveLanguage = cell;
            
            _localisationController.LoadLocalisationData();

            OnClickEvent?.Invoke();
        }
    }
}