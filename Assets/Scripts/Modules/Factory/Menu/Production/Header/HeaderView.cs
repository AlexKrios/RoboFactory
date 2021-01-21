using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Header
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Header View")]
    public class HeaderView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly ProductionMenuManager _productionMenuManager;

        #endregion

        #region Components

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI productGroup;
        [SerializeField] private TextMeshProUGUI productType;
        
        [Space]
        [SerializeField] private List<HeaderTabCellView> tabs;

        #endregion
        
        #region Variables
        
        public Action OnTabClickEvent { get; set; }

        private HeaderTabCellView _activeTab;
        private HeaderTabCellView ActiveTab
        {
            get => _activeTab;
            set 
            {
                if (_activeTab != null)
                    _activeTab.SetInactive();

                _activeTab = value;
                _activeTab.SetActive();
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetTabsData();
            SetData();
        }

        #endregion

        public void SetData()
        {
            var productKey = LocalisationKeys.ProductKeys[_productionMenuManager.ActiveProductGroup];
            var unitKey = LocalisationKeys.UnitKeys[_productionMenuManager.ActiveUnitType];

            title.text = _localisationController.GetLanguageValue(LocalisationKeys.ProductionMenuTitleKey);
            productGroup.text = _localisationController.GetLanguageValue(productKey);
            productType.text = _localisationController.GetLanguageValue(unitKey);
        }
        
        private void SetTabsData()
        {
            var productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length;
            for (var i = 1; i < productGroupCount; i++)
            {
                tabs[i - 1].SetTabData((ProductGroup) i);
                tabs[i - 1].OnClickEvent += OnTabClick;
            }
            
            ActiveTab = tabs.First();
        }
        
        private void OnTabClick(HeaderTabCellView tab, ProductGroup group)
        {
            if (ActiveTab == tab)
                return;

            _productionMenuManager.ActiveProductGroup = group;
            _productionMenuManager.ActiveProductType = 1;

            ActiveTab = tab;

            OnTabClickEvent?.Invoke();
        }
    }
}
