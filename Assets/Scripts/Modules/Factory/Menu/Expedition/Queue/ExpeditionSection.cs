﻿using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Modules.Factory.Menu.Expedition.Queue.Cell;
using Modules.General.Location;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Queue/Expedition Section")]
    public class ExpeditionSection : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly IExpeditionController _expeditionController;

        #endregion

        #region Components
        
        [SerializeField] private ExpeditionCell cellPrototype;

        #endregion

        #region Variables
        
        private List<ExpeditionCell> _cells;
        
        private CanvasGroup _canvasGroup;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _cells = new List<ExpeditionCell>();

            _expeditionController.OnExpeditionComplete += InitCells;
        }

        private void OnEnable()
        {
            InitCells();
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f);
        }
        
        private void OnDisable()
        {
            RemoveCells();
        }
        
        private void OnDestroy()
        {
            _expeditionController.OnExpeditionComplete -= InitCells;
        }

        #endregion

        private void InitCells()
        {
            if (_cells.Count != 0)
                RemoveCells();

            var expeditions = _expeditionController.GetAllExpeditions()
                .OrderBy(x => x.TimeEnd).ToList();

            for (var i = 0; i < _expeditionController.CellCount; i++)
            {
                var cell = CreateCell();
                if (i < expeditions.Count)
                    cell.SetData(expeditions[i]);
            }
        }

        private ExpeditionCell CreateCell()
        {
            var cell = _container.InstantiatePrefabForComponent<ExpeditionCell>(cellPrototype, transform);
            _cells.Add(cell);
            
            return cell;
        }

        private void RemoveCells()
        {
            _cells.ForEach(x => Destroy(x.gameObject));
            _cells.Clear();
        }

        public void IncreaseCellCount()
        {
            _expeditionController.CellCount++;
        }
    }
}
