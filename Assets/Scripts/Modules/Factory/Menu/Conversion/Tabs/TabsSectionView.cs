using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Raw;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Conversion.Tabs
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Tabs Section View")]
    public class TabsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ConversionMenuManager _conversionMenuManager;
        [Inject] private readonly IRawController _rawController;
        
        #endregion
        
        #region Components
        
        [SerializeField] private List<TabCellView> tabs;

        #endregion

        #region Variables

        public Action OnClickEvent { get; set; }

        private TabCellView _activeTab;
        public TabCellView ActiveTab
        {
            get => _activeTab;
            private set
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
            _conversionMenuManager.Tabs = this;

            tabs.ForEach(x => x.OnTabClick += OnTabClick);
        }

        #endregion
        
        private void OnTabClick(TabCellView tab)
        {
            if (ActiveTab == tab)
                return;

            ActiveTab = tab;

            OnClickEvent?.Invoke();
        }
        
        public void SetData()
        {
            var mainRaw = _rawController.GetMainRaw();
            foreach (var rawData in mainRaw)
            {
                var tab = tabs.First(x => x.RawType == rawData.RawType);
                tab.SetTabData(rawData);
            }

            ActiveTab = tabs.First();
        }
    }
}