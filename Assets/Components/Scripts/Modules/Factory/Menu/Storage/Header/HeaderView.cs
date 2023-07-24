using System;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    public class HeaderView : MonoBehaviour
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _productGroup;
        [SerializeField] private Toggle _defaultToggle;

        public Action OnToggleClickEvent { get; set; }
        
        private StorageMenuView _menu;

        private void Awake()
        {
            _defaultToggle.onValueChanged.AddListener(OnToggleClick);
        }

        private void OnDestroy()
        {
            _defaultToggle.onValueChanged.RemoveListener(OnToggleClick);
        }

        public void Initialize()
        {
            _menu = _uiController.FindUi<StorageMenuView>();
            
            SetData();
        }

        private void OnToggleClick(bool isOn)
        {
            _menu.IsDefault = isOn;
            
            OnToggleClickEvent?.Invoke();
        }
        
        public void SetData()
        {
            var productKey = LocalizationKeys.ProductKeys[_menu.ActiveProductGroup];

            _title.text = _localizationService.GetLanguageValue(LocalizationKeys.StorageMenuTitleKey);
            _productGroup.text = _localizationService.GetLanguageValue(productKey);
        }
    }
}
