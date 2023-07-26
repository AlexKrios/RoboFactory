using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    public class ItemsSectionView : MonoBehaviour
    {
        [Inject] private readonly ProductsService productsService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly StorageMenuFactory _storageMenuFactory;
        
        [SerializeField] private Transform _parent;
        [SerializeField] private List<ItemCellView> _items;
        [SerializeField] private TMP_Text _empty;
        
        public List<ItemCellView> Items => _items;

        public Action OnTabClickEvent { get; set; }

        private StorageMenuView _menu;
        private ItemCellView _activeCell;
        private ItemCellView ActiveCell
        {
            get => _activeCell;
            set
            {
                if (_activeCell != null)
                    _activeCell.SetInactive();

                _activeCell = value;
                _menu.ActiveItem = value.Data;
                _activeCell.SetActive();
            }
        }

        public void Initialize()
        {
            _menu = _uiController.FindUi<StorageMenuView>();

            CreateItemCells();
        }

        public void CreateItemCells()
        {
            if (_items.Count != 0)
                RemoveItemCells();
            
            var allItems = GetFilteredItems()
                .Where(x => !x.IsEmpty() || x.ProductType == 0 && _menu.IsDefault).ToList();
            _empty.gameObject.SetActive(allItems.Count == 0);
            if (allItems.Count == 0)
                return;

            foreach (var itemData in allItems)
            {
                var item = _storageMenuFactory.CreateItem(_parent);
                item.OnTabClickEvent += OnTabClick;
                item.SetItemData(itemData);
                _items.Add(item);
            }

            ActiveCell = _items.First();
        }
        
        private List<ProductObject> GetFilteredItems()
        {
            if (_menu.ActiveProductGroup == ProductGroup.All)
                return productsService.GetAllProducts();

            return productsService.GetAllProducts()
                .Where(x => x.ProductGroup == _menu.ActiveProductGroup).ToList();
        }

        private void RemoveItemCells()
        {
            _items.ForEach(x => Destroy(x.gameObject));
            _items.Clear();
        }
        
        private void OnTabClick(ItemCellView tab)
        {
            if (ActiveCell == tab)
                return;
            
            ActiveCell = tab;
            _menu.ActiveItem = tab.Data;
            
            OnTabClickEvent?.Invoke();
        }
    }
}