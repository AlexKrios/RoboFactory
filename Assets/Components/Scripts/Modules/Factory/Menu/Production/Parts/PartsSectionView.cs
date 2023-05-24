using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Parts Section View")]
    public class PartsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text star;
        [SerializeField] private List<PartCellView> parts;
        
        [Space]
        [SerializeField] private Image levelBar;
        [SerializeField] private TMP_Text levelCount;

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
            await SetProductIcon();
            star.text = _menu.ActiveStar.ToString();
            SetLevelData();
            parts.ForEach(x => x.SetPartInfo(ActiveProduct.Recipe));
        }

        private async UniTask SetProductIcon()
        {
            icon.color = new Color(1, 1, 1, 0);
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(ActiveProduct.IconRef);
            icon.DORestart();
            icon.DOFade(1f, 0.1f);
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