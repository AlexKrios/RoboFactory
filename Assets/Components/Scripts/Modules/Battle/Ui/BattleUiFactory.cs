using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    [UsedImplicitly]
    public class BattleUiFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;

        #endregion                

        public QueueCellView CreateQueueCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<QueueCellView>(_settings.QueueCellPrefab, parent);
        }
        
        public UnitCellView CreateUnitCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<UnitCellView>(_settings.UnitCellPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public GameObject _queueCellPrefab;
            
            [Space]
            public GameObject _unitCellPrefab;
            
            public GameObject QueueCellPrefab => _queueCellPrefab;
            public GameObject UnitCellPrefab => _unitCellPrefab;
        }
    }
}
