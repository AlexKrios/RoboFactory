﻿using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Units Section View")]
    public class UnitsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;

        #endregion
        
        #region Components
        
        [SerializeField] private List<UnitCellView> units;

        #endregion

        #region Variables

        public Action EventClick { get; set; }
        
        public UnitCellView ActiveUnit { get; private set; }

        #endregion

        public void Initialize()
        {
            SetData();
        }
        
        private void SetData()
        {
            foreach (var ally in units)
            {
                ally.OnClickEvent += OnUnitClick;
            }
            
            ActiveUnit = units.First();
        }

        public List<UnitCellView> GetUnitsWithData()
        {
            return units.Where(x => x.Data != null).ToList();
        }

        public bool IsAllUnitEmpty()
        {
            return units.All(x => x.Data == null);
        }
        
        private void OnUnitClick(UnitCellView cell)
        {
            ActiveUnit = cell;
            _expeditionMenuFactory.CreateSelectionMenu(_uiController.GetCanvas(CanvasType.Ui).transform);
        }
    }
}
