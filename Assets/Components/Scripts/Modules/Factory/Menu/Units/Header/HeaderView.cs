using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    public class HeaderView : MonoBehaviour
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _unitType;

        private UnitsMenuView _menu;

        public void Initialize()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            SetData();
        }

        public void SetData()
        {
            _title.text = _localizationService.GetLanguageValue(LocalizationKeys.UnitsMenuTitleKey);
            _unitType.text = _localizationService.GetLanguageValue(LocalizationKeys.UnitKeys[_menu.ActiveUnitType]);
        }
    }
}