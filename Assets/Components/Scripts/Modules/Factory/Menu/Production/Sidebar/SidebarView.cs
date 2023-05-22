using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Sidebar View")]
    public class SidebarView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly LocalisationManager _localisationController;
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
            
            icon.color = new Color(1, 1, 1, 0);
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(_menu.ActiveProduct.IconRef);
            icon.DORestart();
            icon.DOFade(1f, 0.1f);

            timer.text = TimeUtil.DateCraftTimer(_menu.ActiveProduct.Recipe.CraftTime);

            foreach (var specData in _menu.ActiveProduct.Recipe.Specs)
            {
                var spec = specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}
