using RoboFactory.General.Localisation;
using RoboFactory.General.Location;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Expedition Menu")]
    public class ExpeditionMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
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
            
            units.EventClick += OnUnitClick;
            locations.EventClick += OnLocationClick;
            start.EventClick += Close;

            title.text = _localisationController.GetLanguageValue(LocalisationKeys.UnitsMenuTitleKey);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            units.EventClick -= OnUnitClick;
            locations.EventClick -= OnLocationClick;
            start.EventClick -= Close;
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
