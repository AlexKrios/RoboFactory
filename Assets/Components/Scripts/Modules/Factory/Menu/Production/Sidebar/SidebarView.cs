using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RoboFactory.General.Asset;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class SidebarView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _timer;
        [SerializeField] private List<SpecCellView> _specs;
        
        private ProductionMenuView _menu;

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
            _title.text = _localizationService.GetLanguageValue(_menu.ActiveProduct.Key);
            
            _icon.color = new Color(1, 1, 1, 0);
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(_menu.ActiveProduct.IconRef);
            _icon.DORestart();
            _icon.DOFade(1f, 0.1f);

            _timer.text = TimeUtil.DateCraftTimer(_menu.ActiveProduct.Recipe.CraftTime);

            foreach (var specData in _menu.ActiveProduct.Recipe.Specs)
            {
                var spec = _specs.First(x => x.SpecType == specData.Type);
                spec.SetData(specData);
            }
        }
    }
}
