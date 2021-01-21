using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Item.Products.Models.Types;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Storage.Items
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Items Section View")]
    public class ItemsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly StorageMenuFactory _storageMenuFactory;
        [Inject] private readonly StorageMenuManager _storageMenuManager;

        #endregion
        
        #region Components

        [SerializeField] private Transform parent;
        [SerializeField] private List<ItemCellView> items;
        [SerializeField] private TextMeshProUGUI empty;
        
        public List<ItemCellView> Items => items;

        #endregion

        #region Variables

        public Action OnTabClickEvent { get; set; }

        private ItemCellView _activeCell;
        public ItemCellView ActiveCell
        {
            get => _activeCell;
            private set
            {
                if (_activeCell != null)
                    _activeCell.SetInactive();

                _activeCell = value;
                _activeCell.SetActive();
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _storageMenuManager.Items = this;
            
            CreateItemCells();
        }

        #endregion
        
        public void SubscribeItemToList(ItemCellView item)
        {
            item.OnTabClickEvent += OnTabClick;
            items.Add(item);
        }

        public void CreateItemCells()
        {
            if (items.Count != 0)
                RemoveItemCells();
            
            var allItems = GetFilteredItems()
                .Where(x => !x.IsEmpty() || x.ProductType == 0 && _storageMenuManager.IsDefault).ToList();
            empty.gameObject.SetActive(allItems.Count == 0);
            if (allItems.Count == 0)
                return;

            foreach (var itemData in allItems)
            {
                var item = _storageMenuFactory.CreateItem(parent);
                item.SetItemData(itemData);
            }

            ActiveCell = items.First();
        }
        
        private List<ProductObject> GetFilteredItems()
        {
            if (_storageMenuManager.ActiveProductGroup == ProductGroup.All)
                return _productsController.GetAllProducts();

            return _productsController.GetAllProducts()
                .Where(x => x.ProductGroup == _storageMenuManager.ActiveProductGroup).ToList();
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
            
            OnTabClickEvent?.Invoke();
        }
    }
}