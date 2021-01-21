using System.Collections.Generic;
using System.Linq;
using Modules.Factory.Menu.Units.Roster;
using Modules.General.Item.Products;
using Modules.General.Save;
using Modules.General.Ui;
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
        [Inject] private readonly UnitsMenuManager _unitsMenuManager;

        #endregion

        #region Components

        [SerializeField] private List<EquipmentCellView> equipment;
        [SerializeField] private Transform modelParent;

        #endregion
        
        #region Variables
        
        public EquipmentCellView ActiveCell { get; private set; }
        
        private GameObject _unitModel;
        
        private RosterCellView ActiveUnit => _unitsMenuManager.Roster.ActiveUnit;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _unitsMenuManager.Info = this;
            
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
            var outfit = ActiveUnit.UnitData.Outfit;
            for (var i = 0; i < outfit.Count; i++)
            {
                if (outfit[i] != Constants.EmptyOutfit)
                {
                    var product = _productsController.GetProduct(outfit[i]);
                    equipment.First(x => x.EquipmentType == product.ProductGroup)
                        .SetEquipmentData(product);
                }
                else
                {
                    equipment[i].ResetEquipmentData();
                }
            }
        }

        private void OnEquipmentClick(EquipmentCellView cell)
        {
            if (cell.Data.ProductType != 0)
            {
                ActiveUnit.UnitData.Outfit[(int)cell.Data.ProductGroup] = Constants.EmptyOutfit;
                
                cell.Data.IncrementCount();
                cell.ResetEquipmentData();
            
                _saveController.SaveStores(true);
                _saveController.SaveUnits();
            }
            else
            {
                ActiveCell = cell;
                _unitsMenuFactory.CreateSelectionMenu(_uiController.FindCanvas(CanvasType.Menu).transform);
            }
        }
        
        private void CreateModel()
        {
            if (_unitModel != null)
                RemoveModel();

            _unitModel = _container.InstantiatePrefab(ActiveUnit.UnitData.Model, modelParent);
            _container.InstantiateComponent<UnitModel>(_unitModel);
        }

        private void RemoveModel()
        {
            Destroy(_unitModel.gameObject);
            _unitModel = null;
        }
    }
}