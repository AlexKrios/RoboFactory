using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Raw;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    public class TabsSectionView : MonoBehaviour
    {
        [Inject] private readonly RawService _rawService;
        
        [SerializeField] private List<TabCellView> _tabs;

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

        private void Awake()
        {
            _tabs.ForEach(x => x.OnTabClick += OnTabClick);
        }

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