﻿using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Item.Products;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Unit;
using Components.Scripts.Modules.General.Unit.Object;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Units.Info
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Info View")]
    public class InfoView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;

        #endregion

        #region Components

        [SerializeField] private List<EquipmentCellView> equipment;
        [SerializeField] private Transform modelParent;

        #endregion
        
        #region Variables
        
        private UnitsMenuView _menu;
        public EquipmentCellView ActiveCell { get; private set; }
        public UnitViewObject UnitModel { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            equipment.ForEach(x => x.OnClickEvent += OnEquipmentClick);

            SetData();
        }

        #endregion

        public void SetData()
        {
            SetEquipmentData();
            CreateModel();
        }
        
        private void SetEquipmentData()
        {
            var outfit = _menu.ActiveUnit.Outfit;
            foreach (var data in outfit)
            {
                var product = _productsManager.GetProduct(data.Value);
                equipment.First(x => x.EquipmentType == product.ProductGroup)
                    .SetEquipmentData(product);
            }
        }

        private async void OnEquipmentClick(EquipmentCellView cell)
        {
            //TODO объяеденить плюс и минус предмет
            if (cell.Data.ProductType != 0)
            {
                await _productsManager.AddItem(cell.Data.Key);
                cell.ResetEquipmentData();
                
                await _unitsManager.SetEquipment(_menu.ActiveUnit.Key, cell.Data.ProductGroup, cell.Data.Key);
                UnitModel.SetEquipment(cell.Data);
            }
            else
            {
                ActiveCell = cell;
                _menu.ActiveEquipment = cell.Data;
                _unitsMenuFactory.CreateSelectionMenu(_uiController.GetCanvas(CanvasType.Ui).transform);
            }
        }
        
        private void CreateModel()
        {
            if (UnitModel != null)
                RemoveModel();

            UnitModel = _container.InstantiatePrefabForComponent<UnitViewObject>(_menu.ActiveUnit.Model, modelParent);
            UnitModel.SetData(_menu.ActiveUnit);
            _container.InstantiateComponent<UnitModel>(UnitModel.gameObject);
        }

        private void RemoveModel()
        {
            Destroy(UnitModel.gameObject);
            UnitModel = null;
        }
    }
}