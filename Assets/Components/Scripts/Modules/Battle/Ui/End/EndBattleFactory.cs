using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    [UsedImplicitly]
    public class EndBattleFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;

        #endregion                

        public EndBattlePopupView CreateEndPopup(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<EndBattlePopupView>(_settings.PopupPrefab, parent);
        }
        
        public EndBattleItemView CreateItemCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<EndBattleItemView>(_settings.ItemCellPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _popupPrefab;
            [SerializeField] private GameObject _itemCellPrefab;
            
            public GameObject PopupPrefab => _popupPrefab;
            public GameObject ItemCellPrefab => _itemCellPrefab;
        }
    }
}