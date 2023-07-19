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
        
        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _star;
        [SerializeField] private List<PartCellView> _parts;
        
        [Space]
        [SerializeField] private Image _levelBar;
        [SerializeField] private TMP_Text _levelCount;

        #endregion

        #region Variables

        public Action OnPartClickEvent { get; set; }

        private ProductionMenuView _menu;
        private ProductObject ActiveProduct => _menu.ActiveProduct;

        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();

            SetData();
        }
        
        public async void SetData()
        {
            await SetProductIcon();
            _star.text = _menu.ActiveStar.ToString();
            SetLevelData();
            _parts.ForEach(x => x.SetPartInfo(ActiveProduct.Recipe));
        }

        private async UniTask SetProductIcon()
        {
            _icon.color = new Color(1, 1, 1, 0);
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(ActiveProduct.IconRef);
            _icon.DORestart();
            _icon.DOFade(1f, 0.1f);
        }

        private void SetLevelData()
        {
            var level = ActiveProduct.GetLevel();
            var experience = ActiveProduct.Experience;

            if (ActiveProduct.Caps.Last().level <= level)
            {
                _levelBar.fillAmount = 1f;
                _levelCount.text = (level + 1).ToString();
            }
            else
            {
                var prevCup = level == 1 ? 0 : ActiveProduct.Caps.First(x => x.level == level - 1).experience;
                var currentCap = level == 1 
                    ? ActiveProduct.Caps.First().experience  
                    : ActiveProduct.Caps.First(x => x.level == level).experience;
                var step = 1f / (currentCap - prevCup);

                _levelBar.fillAmount = step * (experience - prevCup);
                
                _levelCount.text = level.ToString();
            }
        }
    }
}