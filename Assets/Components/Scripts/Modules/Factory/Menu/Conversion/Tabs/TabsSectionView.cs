using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Raw;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Tabs Section View")]
    public class TabsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly RawService _rawService;
        
        #endregion
        
        #region Components
        
        [SerializeField] private List<TabCellView> _tabs;

        #endregion

        #region Variables

        public Action OnClickEvent { get; set; }

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
            _tabs.ForEach(x => x.OnTabClick += OnTabClick);
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
            var mainRaw = _rawService.GetAllRaw();
            foreach (var rawData in mainRaw)
            {
                var tab = _tabs.First(x => x.RawType == rawData.RawType);
                tab.SetTabData(rawData);
            }

            ActiveTab = _tabs.First();
        }
    }
}