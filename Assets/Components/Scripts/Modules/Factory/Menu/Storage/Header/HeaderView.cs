using System;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Header View")]
    public class HeaderView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text productGroup;
        [SerializeField] private Toggle defaultToggle;

        #endregion

        #region Variables

        public Action OnToggleClickEvent { get; set; }
        
        private StorageMenuView _menu;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            defaultToggle.onValueChanged.AddListener(OnToggleClick);
        }

        private void OnDestroy()
        {
            defaultToggle.onValueChanged.RemoveListener(OnToggleClick);
        }

        #endregion

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

            title.text = localizationController.GetLanguageValue(LocalizationKeys.StorageMenuTitleKey);
            productGroup.text = localizationController.GetLanguageValue(productKey);
        }
    }
}
