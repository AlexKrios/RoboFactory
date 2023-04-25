 using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Products;
using Modules.General.Save;
using Modules.General.Ui;
 using Modules.General.Unit.Object;
 using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units.Info
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Info View")]
    public class InfoView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly ISaveController _saveController;
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
                var product = _productsController.GetProduct(data);
                equipment.First(x => x.EquipmentType == product.ProductGroup)
                    .SetEquipmentData(product);
            }
        }

        private void OnEquipmentClick(EquipmentCellView cell)
        {
            if (cell.Data.ProductType != 0)
            {
                cell.Data.IncrementCount();
                cell.ResetEquipmentData();
                
                _menu.ActiveUnit.Outfit[(int)cell.Data.ProductGroup - 1] = cell.Data.Key;
                UnitModel.SetEquipment(cell.Data);
            
                _saveController.SaveStores(true);
                _saveController.SaveUnits();
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