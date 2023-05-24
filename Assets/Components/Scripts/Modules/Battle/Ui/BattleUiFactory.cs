using System;
using UnityEngine;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    public class BattleUiFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;

        #endregion                

        public QueueCellView CreateQueueCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<QueueCellView>(_settings.queueCellPrefab, parent);
        }
        
        public UnitCellView CreateUnitCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<UnitCellView>(_settings.unitCellPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public string queueCellName;
            public GameObject queueCellPrefab;
            
            [Space]
            public string unitCellName;
            public GameObject unitCellPrefab;
        }
    }
}
