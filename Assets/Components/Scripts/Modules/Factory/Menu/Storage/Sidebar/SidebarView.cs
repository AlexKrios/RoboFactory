using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Asset;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Sidebar View")]
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

        [Space]
        [SerializeField] private List<SpecCellView> specs;

        #endregion

        #region Variables

        private StorageMenuView _menu;

        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<StorageMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            gameObject.SetActive(!_menu.IsItemEmpty);
            if (_menu.IsItemEmpty)
                return;
            
            title.text = _localisationController.GetLanguageValue(_menu.ActiveItem.Key);
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(_menu.ActiveItem.IconRef);

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