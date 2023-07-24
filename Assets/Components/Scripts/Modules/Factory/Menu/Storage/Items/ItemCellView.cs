using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Storage
{
    public class ItemCellView : CellBase
    {
        [Space]
        [SerializeField] private GameObject _countWrapper;
        [SerializeField] private TMP_Text _countText;
        
        public Action<ItemCellView> OnTabClickEvent { get; set; }

        public ProductObject Data { get; private set; }

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
            _countWrapper.SetActive(Data.ProductType != 0);
            _countText.text = $"x{Data.Count}";
        }
    }
}