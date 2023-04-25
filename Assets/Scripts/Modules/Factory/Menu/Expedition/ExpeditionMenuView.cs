using Modules.Factory.Menu.Expedition.Locations;
using Modules.Factory.Menu.Expedition.Sidebar;
using Modules.Factory.Menu.Expedition.Units;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Location.Model;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Expedition Menu")]
    public class ExpeditionMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;

        #endregion

        #region Components
        
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private StarButtonView star;
        [SerializeField] private UnitsSectionView units;
        [SerializeField] private LocationsSectionView locations;
        [SerializeField] private SidebarView sidebar;
        [SerializeField] private StartButtonView start;
        
        public UnitsSectionView Units => units;

        #endregion

        #region Variables

        public LocationObject ActiveLocation { get; set; }
        public int ActiveStar { get; set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            UiController.AddUi(this);
            
            units.OnClickEvent += OnUnitClick;
            locations.OnClickEvent += OnLocationClick;

            title.text = _localisationController.GetLanguageValue(LocalisationKeys.UnitsMenuTitleKey);
        }

        #endregion

        #region Click Handlers

        private void OnUnitClick()
        {
            sidebar.SetData();
            star.ResetStar();
            start.SetState();
        }
        
        private void OnLocationClick()
        {
            sidebar.SetData();
            star.ResetStar();
        }

        #endregion
    }
}
