using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Ui;
using TMPro;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Header
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Header View")]
    public class HeaderView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text productGroup;
        [SerializeField] private TMP_Text productType;
        
        [Space]
        [SerializeField] private List<HeaderTabCellView> tabs;

        #endregion
        
        #region Variables
        
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
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            SetTabsData();
            SetData();
        }

        #endregion

        public void SetData()
        {
            var productKey = LocalisationKeys.ProductKeys[_menu.ActiveProductGroup];
            var unitKey = LocalisationKeys.UnitKeys[_menu.ActiveUnitType];

            title.text = _localisationController.GetLanguageValue(LocalisationKeys.ProductionMenuTitleKey);
            productGroup.text = _localisationController.GetLanguageValue(productKey);
            productType.text = _localisationController.GetLanguageValue(unitKey);
        }

        private void SetTabsData()
        {
            var productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length - 1;
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

            _menu.ActiveProductGroup = group;
            _menu.ActiveProductType = 1;

            ActiveTab = tab;

            OnTabClickEvent?.Invoke();
        }
    }
}
