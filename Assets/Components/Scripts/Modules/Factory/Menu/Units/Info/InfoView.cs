using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    public class InfoView : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsService _productsService;
        [Inject] private readonly UnitsService _unitsService;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;
        [Inject(Id = Constants.PopupsParentKey)] private readonly Transform _popupsParent;

        [SerializeField] private List<EquipmentCellView> _equipment;
        [SerializeField] private Transform _modelParent;
        
        private UnitsMenuView _menu;
        public EquipmentCellView ActiveCell { get; private set; }
        public UnitViewObject UnitModel { get; private set; }

        public void Initialize()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            _equipment.ForEach(x =>
            {
                x.OnClickEvent += OnEquipmentClick;
                x.Initialize();
            });
            
            SetData();
        }

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
                var product = _productsService.GetProduct(data.Value);
                _equipment.First(x => x.EquipmentType == product.ProductGroup)
                    .SetEquipmentData(product);
            }
        }

        private async void OnEquipmentClick(EquipmentCellView cell)
        {
            //TODO объяеденить плюс и минус предмет
            if (cell.Data.ProductType != 0)
            {
                await _productsService.AddItem(cell.Data.Key);
                cell.ResetEquipmentData();
                
                await _unitsService.SetEquipment(_menu.ActiveUnit.Key, cell.Data.ProductGroup, cell.Data.Key);
                UnitModel.SetEquipment(cell.Data);
            }
            else
            {
                ActiveCell = cell;
                _menu.ActiveEquipment = cell.Data;
                _unitsMenuFactory.CreateSelectionMenu(_popupsParent);
            }
        }
        
        private void CreateModel()
        {
            if (UnitModel != null)
                RemoveModel();

            //TODO: Добавить в адресблы
            UnitModel = _container.InstantiatePrefabForComponent<UnitViewObject>(_menu.ActiveUnit.Model, _modelParent);
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