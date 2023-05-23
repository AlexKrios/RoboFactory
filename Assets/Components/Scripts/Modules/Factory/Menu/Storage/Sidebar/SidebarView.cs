﻿using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Storage.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text title;
        [SerializeField] private Image icon;

        [Space]
        [SerializeField] private List<SpecCellView> specs;

        #endregion

        #region Variables

        private StorageMenuView _menu;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<StorageMenuView>();
            
            SetData();
        }

        #endregion

        public async void SetData()
        {
            gameObject.SetActive(!_menu.IsItemEmpty);
            if (_menu.IsItemEmpty)
                return;
            
            title.text = _localisationController.GetLanguageValue(_menu.ActiveItem.Key);
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(_menu.ActiveItem.IconRef);

            SetSpecsData();
        }

        private void SetSpecsData()
        {
            foreach (var specData in _menu.ActiveItem.Recipe.Specs)
            {
                var spec = specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}