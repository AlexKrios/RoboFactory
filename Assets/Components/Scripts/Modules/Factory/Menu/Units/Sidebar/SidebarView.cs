using System.Collections.Generic;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsManager _productsManager;

        #endregion

        #region Components

        [SerializeField] private TMP_Text _unitName;
        [SerializeField] private Image _unitIcon;
        [SerializeField] private List<SpecCellView> _specs;

        #endregion
        
        #region Variables
        
        private UnitsMenuView _menu;

        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            _unitName.text = localizationController.GetLanguageValue(_menu.ActiveUnit.Key);
            _unitIcon.sprite = await addressableService.LoadAssetAsync<Sprite>(_menu.ActiveUnit.IconRef);

            for (var i = 0; i < _menu.ActiveUnit.Outfit.Count; i++)
            {
                var spec = _menu.ActiveUnit.Specs[(SpecType) i];
                var specObject = new SpecObject();
                foreach (var outfit in _menu.ActiveUnit.Outfit)
                {
                    var item = _productsManager.GetProduct(outfit.Value).Recipe;

                    spec += item.Specs[i].value;
                }

                specObject.type = (SpecType) i;
                specObject.value = spec;
                
                _specs[i].SetData(specObject);
            }
        }
    }
}