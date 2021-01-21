using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Production.Parts
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Parts Section View")]
    public class PartsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly ProductionMenuManager _productionMenuManager;

        #endregion

        #region Components

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI star;
        [SerializeField] private List<PartCellView> parts;
        
        [Space]
        [SerializeField] private Image levelBar;
        [SerializeField] private TextMeshProUGUI levelCount;

        #endregion

        #region Variables

        public Action OnPartClickEvent { get; set; }
        
        public ProductObject ActiveItem { get; private set; }
        private ProductObject ActiveProduct => _productionMenuManager.Products.ActiveProduct.Data;
        private int ActiveStar => _productionMenuManager.Star.ActiveStar;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _productionMenuManager.Parts = this;

            SetData();
        }

        #endregion

        public async void SetData()
        {
            ActiveItem = _productsController.GetProduct(ActiveProduct.Key);
            
            icon.sprite = await AssetsController.LoadAsset<Sprite>(ActiveItem.IconRef);
            star.text = ActiveStar.ToString();
            SetLevelData();

            var recipe = ActiveItem.Recipe;
            parts.ForEach(x => x.SetPartInfo(recipe));
        }
        
        public async void SetComponent()
        {
            icon.sprite = await AssetsController.LoadAsset<Sprite>(ActiveItem.IconRef);
            star.text = ActiveStar.ToString();
            SetLevelData();
            
            var recipe = ActiveItem.Recipe;
            parts.ForEach(x => x.SetPartInfo(recipe));
        }

        private void SetLevelData()
        {
            var level = ActiveItem.GetLevel();
            var experience = ActiveItem.Experience;

            if (ActiveItem.Caps.Last().level <= level)
            {
                levelBar.fillAmount = 1f;
                levelCount.text = (level + 1).ToString();
            }
            else
            {
                var prevCup = level == 1 ? 0 : ActiveItem.Caps.First(x => x.level == level - 1).experience;
                var currentCap = level == 1 
                    ? ActiveItem.Caps.First().experience  
                    : ActiveItem.Caps.First(x => x.level == level).experience;
                var step = 1f / (currentCap - prevCup);

                levelBar.fillAmount = step * (experience - prevCup);
                
                levelCount.text = level.ToString();
            }
        }
    }
}