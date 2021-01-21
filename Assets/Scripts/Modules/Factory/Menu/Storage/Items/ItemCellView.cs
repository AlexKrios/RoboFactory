using System;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Storage.Items
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Item Cell View")]
    public class ItemCellView : CellBase
    {
        #region Zenject

        [Inject] private readonly StorageMenuManager _storageMenuManager;

        #endregion

        #region Components

        [SerializeField] private GameObject countWrapper;
        [SerializeField] private TMP_Text countText;

        #endregion
        
        #region Variables

        public Action<ItemCellView> OnTabClickEvent { get; set; }

        public ProductObject ItemData { get; private set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _storageMenuManager.Items.SubscribeItemToList(this);
        }

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnTabClickEvent?.Invoke(this);
        }
        
        public async void SetItemData(ProductObject item)
        {
            ItemData = item; 
            var sprite = await AssetsController.LoadAsset<Sprite>(item.IconRef);

            SetIconSprite(sprite);
            SetCount();
        }

        private void SetCount()
        {
            countWrapper.SetActive(ItemData.ProductType != 0);
            countText.text = $"x{ItemData.Count}";
        }
    }
}