using System;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Battle.Ui.End
{
    public class EndBattleFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;

        #endregion                

        public EndBattlePopupView CreateEndPopup(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<EndBattlePopupView>(_settings.popupPrefab, parent);
        }
        
        public EndBattleItemView CreateItemCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<EndBattleItemView>(_settings.itemCellPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public string popupName;
            public GameObject popupPrefab;
            
            [Space]
            public string itemCellName;
            public GameObject itemCellPrefab;
        }
    }
}