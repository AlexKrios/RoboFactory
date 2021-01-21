using System.Collections.Generic;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Localisation;
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
        [Inject] private readonly ProductionMenuManager _productionMenuManager;

        #endregion

        #region Components
        
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private List<SpecCellView> specs;

        #endregion
        
        #region Variables

        private ProductObject ActiveItem => _productionMenuManager.Parts.ActiveItem;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetData();
        }

        #endregion

        public async void SetData()
        {
            title.text = _localisationController.GetLanguageValue(ActiveItem.Key);
            icon.sprite = await AssetsController.LoadAsset<Sprite>(ActiveItem.IconRef);
            timer.text = TimeUtil.DateCraftTimer(ActiveItem.Recipe.CraftTime);

            foreach (var specData in ActiveItem.Recipe.Specs)
            {
                var spec = specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}
