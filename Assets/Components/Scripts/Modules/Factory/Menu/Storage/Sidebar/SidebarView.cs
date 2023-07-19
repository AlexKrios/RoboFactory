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

        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;

        [Space]
        [SerializeField] private List<SpecCellView> _specs;

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
            
            _title.text = localizationController.GetLanguageValue(_menu.ActiveItem.Key);
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(_menu.ActiveItem.IconRef);

            SetSpecsData();
        }

        private void SetSpecsData()
        {
            foreach (var specData in _menu.ActiveItem.Recipe.Specs)
            {
                var spec = _specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}