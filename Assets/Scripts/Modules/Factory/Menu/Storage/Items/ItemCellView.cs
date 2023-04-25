using System;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;

namespace Modules.Factory.Menu.Storage.Items
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Item Cell View")]
    public class ItemCellView : CellBase
    {
        #region Components

        [SerializeField] private GameObject countWrapper;
        [SerializeField] private TMP_Text countText;

        #endregion
        
        #region Variables

        public Action<ItemCellView> OnTabClickEvent { get; set; }

        public ProductObject Data { get; private set; }

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnTabClickEvent?.Invoke(this);
        }
        
        public async void SetItemData(ProductObject item)
        {
            Data = item; 
            var sprite = await AssetsController.LoadAsset<Sprite>(item.IconRef);

            SetIconSprite(sprite);
            SetCount();
        }

        private void SetCount()
        {
            countWrapper.SetActive(Data.ProductType != 0);
            countText.text = $"x{Data.Count}";
        }
    }
}