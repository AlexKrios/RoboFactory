using System.Collections.Generic;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    public class SidebarView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsService productsService;

        [SerializeField] private TMP_Text _unitName;
        [SerializeField] private Image _unitIcon;
        [SerializeField] private List<SpecCellView> _specs;
        
        private UnitsMenuView _menu;

        public void Initialize()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            _unitName.text = _localizationService.GetLanguageValue(_menu.ActiveUnit.Key);
            _unitIcon.sprite = await _addressableService.LoadAssetAsync<Sprite>(_menu.ActiveUnit.IconRef);

            for (var i = 0; i < _menu.ActiveUnit.Outfit.Count; i++)
            {
                var spec = _menu.ActiveUnit.Specs[(SpecType) i];
                var specObject = new SpecObject();
                foreach (var outfit in _menu.ActiveUnit.Outfit)
                {
                    var item = productsService.GetProduct(outfit.Value).Recipe;
                    spec += item.Specs[i].Value;
                }

                specObject.Type = (SpecType) i;
                specObject.Value = spec;
                
                _specs[i].SetData(specObject);
            }
        }
    }
}