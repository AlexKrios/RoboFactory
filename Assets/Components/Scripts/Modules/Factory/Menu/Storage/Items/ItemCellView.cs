using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Storage
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
        
        public void SetItemData(ProductObject item)
        {
            Data = item;
            
            SetIconSprite(item.IconRef);
            SetCount();
        }

        private void SetCount()
        {
            countWrapper.SetActive(Data.ProductType != 0);
            countText.text = $"x{Data.Count}";
        }
    }
}