using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units.Header
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Header View")]
    public class HeaderView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly UnitsMenuManager _unitsMenuManager;

        #endregion

        #region Components

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI unitType;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _unitsMenuManager.Header = this;
            
            SetData();
        }

        #endregion
        
        public void SetData()
        {
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.UnitsMenuTitleKey);
            unitType.text = _localisationController.GetLanguageValue(LocalisationKeys.UnitKeys[_unitsMenuManager.ActiveUnitType]);
        }
    }
}