using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Sidebar View")]
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
        [SerializeField] private TMP_Text _timer;
        [SerializeField] private List<SpecCellView> _specs;

        #endregion
        
        #region Variables

        private ProductionMenuView _menu;

        #endregion

        private void Awake()
        {
            _icon.color = new Color(1, 1, 1, 0);
        }
        
        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            SetData();
        }

        public async void SetData()
        {
            _title.text = localizationController.GetLanguageValue(_menu.ActiveProduct.Key);
            
            _icon.color = new Color(1, 1, 1, 0);
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(_menu.ActiveProduct.IconRef);
            _icon.DORestart();
            _icon.DOFade(1f, 0.1f);

            _timer.text = TimeUtil.DateCraftTimer(_menu.ActiveProduct.Recipe.CraftTime);

            foreach (var specData in _menu.ActiveProduct.Recipe.Specs)
            {
                var spec = _specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}
