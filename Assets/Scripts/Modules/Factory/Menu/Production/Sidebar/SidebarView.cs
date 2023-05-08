using System.Collections.Generic;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Localisation;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Production.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly ILocalisationController _localisationController;
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

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            SetData();
        }

        #endregion

        public async void SetData()
        {
            title.text = _localisationController.GetLanguageValue(_menu.ActiveProduct.Key);
            icon.sprite = await AssetsController.LoadAsset<Sprite>(_menu.ActiveProduct.IconRef);
            timer.text = TimeUtil.DateCraftTimer(_menu.ActiveProduct.Recipe.CraftTime);

            foreach (var specData in _menu.ActiveProduct.Recipe.Specs)
            {
                var spec = specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}
