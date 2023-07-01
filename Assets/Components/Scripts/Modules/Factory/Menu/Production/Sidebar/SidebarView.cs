﻿using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text timer;
        [SerializeField] private List<SpecCellView> specs;

        #endregion
        
        #region Variables

        private ProductionMenuView _menu;

        #endregion

        private void Awake()
        {
            icon.color = new Color(1, 1, 1, 0);
        }
        
        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            title.text = _localisationController.GetLanguageValue(_menu.ActiveProduct.Key);
            
            icon.color = new Color(1, 1, 1, 0);
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(_menu.ActiveProduct.IconRef);
            icon.DORestart();
            icon.DOFade(1f, 0.1f);

            timer.text = TimeUtil.DateCraftTimer(_menu.ActiveProduct.Recipe.CraftTime);

            foreach (var specData in _menu.ActiveProduct.Recipe.Specs)
            {
                var spec = specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}
