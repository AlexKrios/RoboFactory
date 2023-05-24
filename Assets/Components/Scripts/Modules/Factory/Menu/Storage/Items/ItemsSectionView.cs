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
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Items Section View")]
    public class ItemsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly StorageMenuFactory _storageMenuFactory;

        #endregion
        
        #region Components

        [SerializeField] private Transform parent;
        [SerializeField] private List<ItemCellView> items;
        [SerializeField] private TMP_Text empty;
        
        public List<ItemCellView> Items => items;

        #endregion

        #region Variables

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

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<StorageMenuView>();

            CreateItemCells();
        }

        #endregion

        public void CreateItemCells()
        {
            if (items.Count != 0)
                RemoveItemCells();
            
            var allItems = GetFilteredItems()
                .Where(x => !x.IsEmpty() || x.ProductType == 0 && _menu.IsDefault).ToList();
            empty.gameObject.SetActive(allItems.Count == 0);
            if (allItems.Count == 0)
                return;

            foreach (var itemData in allItems)
            {
                var item = _storageMenuFactory.CreateItem(parent);
                item.OnTabClickEvent += OnTabClick;
                item.SetItemData(itemData);
                items.Add(item);
            }

            ActiveCell = items.First();
        }
        
        private List<ProductObject> GetFilteredItems()
        {
            if (_menu.ActiveProductGroup == ProductGroup.All)
                return _productsManager.GetAllProducts();

            return _productsManager.GetAllProducts()
                .Where(x => x.ProductGroup == _menu.ActiveProductGroup).ToList();
        }

        private void RemoveItemCells()
        {
            items.ForEach(x => Destroy(x.gameObject));
            items.Clear();
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