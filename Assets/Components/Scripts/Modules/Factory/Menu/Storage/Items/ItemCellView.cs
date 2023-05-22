using System;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;

namespace Components.Scripts.Modules.Factory.Menu.Storage.Items
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