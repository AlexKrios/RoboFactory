using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Asset;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    public class SidebarView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;

        [Space]
        [SerializeField] private List<SpecCellView> _specs;

        private StorageMenuView _menu;

        public void Initialize()
        {
            _menu = _uiController.FindUi<StorageMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            gameObject.SetActive(!_menu.IsItemEmpty);
            if (_menu.IsItemEmpty) return;
            
            _title.text = _localizationService.GetLanguageValue(_menu.ActiveItem.Key);
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(_menu.ActiveItem.IconRef);

            SetSpecsData();
        }

        private void SetSpecsData()
        {
            foreach (var specData in _menu.ActiveItem.Recipe.Specs)
            {
                var spec = _specs.First(x => x.SpecType == specData.Type);
                spec.SetData(specData);
            }
        }
    }
}