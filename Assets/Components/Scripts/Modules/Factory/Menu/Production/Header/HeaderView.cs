using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class HeaderView : MonoBehaviour
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _productGroup;
        [SerializeField] private TMP_Text _productType;
        
        [Space]
        [SerializeField] private List<HeaderTabCellView> _tabs;
        
        public Action OnTabClickEvent { get; set; }

        private ProductionMenuView _menu;
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
        
        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            SetTabsData();
            SetHeaderData();
        }
        
        public void SetHeaderData()
        {
            var productKey = LocalizationKeys.ProductKeys[_menu.ActiveProductGroup];
            var unitKey = LocalizationKeys.UnitKeys[_menu.ActiveUnitType];

            _title.text = _localizationService.GetLanguageValue(LocalizationKeys.ProductionMenuTitleKey);
            _productGroup.text = _localizationService.GetLanguageValue(productKey);
            _productType.text = _localizationService.GetLanguageValue(unitKey);
        }

        private void SetTabsData()
        {
            var productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length - 1;
            for (var i = 1; i < productGroupCount; i++)
            {
                _tabs[i - 1].SetTabData((ProductGroup) i);
                _tabs[i - 1].OnClickEvent += OnTabClick;
            }
            
            ActiveTab = _tabs.First();
        }
        
        private void OnTabClick(HeaderTabCellView tab, ProductGroup group)
        {
            if (ActiveTab == tab)
                return;

            _menu.ActiveProductGroup = group;
            _menu.ActiveProductType = 1;

            ActiveTab = tab;

            OnTabClickEvent?.Invoke();
        }
    }
}
