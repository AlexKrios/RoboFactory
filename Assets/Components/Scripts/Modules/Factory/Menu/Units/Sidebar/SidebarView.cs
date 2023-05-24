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

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsManager _productsManager;

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
            unitIcon.sprite = await AssetsManager.LoadAsset<Sprite>(_menu.ActiveUnit.IconRef);

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
                
                specs[i].SetData(specObject);
            }
        }
    }
}