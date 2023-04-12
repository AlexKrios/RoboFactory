﻿using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Ui;
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
        
        [Inject] private readonly IUiController _uiController;

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

        private ProductionMenuView _menu;
        private ProductObject ActiveProduct => _menu.ActiveProduct;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();

            SetData();
        }

        #endregion

        public async void SetData()
        {
            icon.sprite = await AssetsController.LoadAsset<Sprite>(ActiveProduct.IconRef);
            star.text = _menu.ActiveStar.ToString();
            SetLevelData();
            parts.ForEach(x => x.SetPartInfo(ActiveProduct.Recipe));
        }

        private void SetLevelData()
        {
            var level = ActiveProduct.GetLevel();
            var experience = ActiveProduct.Experience;

            if (ActiveProduct.Caps.Last().level <= level)
            {
                levelBar.fillAmount = 1f;
                levelCount.text = (level + 1).ToString();
            }
            else
            {
                var prevCup = level == 1 ? 0 : ActiveProduct.Caps.First(x => x.level == level - 1).experience;
                var currentCap = level == 1 
                    ? ActiveProduct.Caps.First().experience  
                    : ActiveProduct.Caps.First(x => x.level == level).experience;
                var step = 1f / (currentCap - prevCup);

                levelBar.fillAmount = step * (experience - prevCup);
                
                levelCount.text = level.ToString();
            }
        }
    }
}