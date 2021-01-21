using System.Collections.Generic;
using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object.Spec;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Localisation;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit.Models.Object;
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
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly UnitsMenuManager _unitsMenuManager;

        #endregion

        #region Components

        [SerializeField] private TextMeshProUGUI unitName;
        [SerializeField] private Image unitIcon;
        [SerializeField] private List<SpecCellView> specs;

        #endregion
        
        #region Variables
        
        private UnitObject UnitData => _unitsMenuManager.Roster.ActiveUnit.UnitData;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _unitsMenuManager.Sidebar = this;
            
            SetData();
        }

        #endregion
        
        public async void SetData()
        {
            unitName.text = _localisationController.GetLanguageValue(UnitData.Key);
            unitIcon.sprite = await AssetsController.LoadAsset<Sprite>(UnitData.IconRef);

            for (var i = 0; i < UnitData.Outfit.Count; i++)
            {
                var spec = UnitData.Specs[(SpecType) i];
                var specObject = new SpecObject();
                foreach (var outfit in UnitData.Outfit)
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