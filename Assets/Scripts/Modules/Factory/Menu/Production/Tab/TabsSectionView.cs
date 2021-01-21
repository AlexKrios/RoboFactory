﻿using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Tabs Section View")]
    public class TabsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ProductionMenuManager _productionMenuManager;

        #endregion

        #region Components

        [SerializeField] private List<TabCellView> tabs;

        #endregion
        
        #region Variables
        
        public Action OnTabClickEvent { get; set; }

        private TabCellView _activeTab;
        private TabCellView ActiveTab
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
            _productionMenuManager.Tabs = this;
            
            SetData();
        }

        #endregion

        public void SetData()
        {
            tabs.ForEach(x =>
            {
                x.SetTabData();
                x.OnClickEvent += OnTabClick;
            });

            ActiveTab = tabs.First();
        }
        
        private void OnTabClick(TabCellView tab, UnitType unit)
        {
            if (ActiveTab == tab)
                return;

            _productionMenuManager.ActiveUnitType = unit;
            _productionMenuManager.ActiveProductType = 1;

            ActiveTab = tab;

            OnTabClickEvent?.Invoke();
        }
    }
}