using System;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
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
        [Inject] private readonly StorageMenuManager _storageMenuManager;

        #endregion

        #region Components

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI productGroup;
        [SerializeField] private Toggle defaultToggle;

        #endregion

        #region Variables

        public Action OnToggleClickEvent { get; set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _storageMenuManager.Header = this;
            
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
            _storageMenuManager.IsDefault = isOn;
            
            OnToggleClickEvent?.Invoke();
        }
        
        public void SetData()
        {
            var productKey = LocalisationKeys.ProductKeys[_storageMenuManager.ActiveProductGroup];

            title.text = _localisationController.GetLanguageValue(LocalisationKeys.StorageMenuTitleKey);
            productGroup.text = _localisationController.GetLanguageValue(productKey);
        }
    }
}
