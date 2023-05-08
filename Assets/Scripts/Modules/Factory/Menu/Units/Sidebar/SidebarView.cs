using System.Collections.Generic;
using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object.Spec;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Localisation;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Units.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductsController _productsController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text unitName;
        [SerializeField] private Image unitIcon;
        [SerializeField] private List<SpecCellView> specs;

        #endregion
        
        #region Variables
        
        private UnitsMenuView _menu;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            SetData();
        }

        #endregion
        
        public async void SetData()
        {
            unitName.text = _localisationController.GetLanguageValue(_menu.ActiveUnit.Key);
            unitIcon.sprite = await AssetsController.LoadAsset<Sprite>(_menu.ActiveUnit.IconRef);

            for (var i = 0; i < _menu.ActiveUnit.Outfit.Count; i++)
            {
                var spec = _menu.ActiveUnit.Specs[(SpecType) i];
                var specObject = new SpecObject();
                foreach (var outfit in _menu.ActiveUnit.Outfit)
                {
                    if (outfit == Constants.EmptyOutfit)
                        continue;

                    var item = _productsController.GetProduct(outfit).Recipe;

                    spec += item.Specs[i].value;
                }

                specObject.type = (SpecType) i;
                specObject.value = spec;
                
                specs[i].SetData(specObject);
            }
        }
    }
}