using System;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Storage.Header
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Header View")]
    public class HeaderView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
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
            _menu = _uiController.FindUi<StorageMenuView>();
            defaultToggle.onValueChanged.AddListener(OnToggleClick);

            SetData();
        }

        private void OnDestroy()
        {
            defaultToggle.onValueChanged.RemoveListener(OnToggleClick);
        }

        #endregion

        private void OnToggleClick(bool isOn)
        {
            _menu.IsDefault = isOn;
            
            OnToggleClickEvent?.Invoke();
        }
        
        public void SetData()
        {
            var productKey = LocalisationKeys.ProductKeys[_menu.ActiveProductGroup];

            title.text = _localisationController.GetLanguageValue(LocalisationKeys.StorageMenuTitleKey);
            productGroup.text = _localisationController.GetLanguageValue(productKey);
        }
    }
}
