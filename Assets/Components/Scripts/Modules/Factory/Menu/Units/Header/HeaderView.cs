using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Header View")]
    public class HeaderView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components

        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text unitType;

        #endregion

        #region Variables

        private UnitsMenuView _menu;

        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            SetData();
        }

        public void SetData()
        {
            title.text = localizationController.GetLanguageValue(LocalizationKeys.UnitsMenuTitleKey);
            unitType.text = localizationController.GetLanguageValue(LocalizationKeys.UnitKeys[_menu.ActiveUnitType]);
        }
    }
}