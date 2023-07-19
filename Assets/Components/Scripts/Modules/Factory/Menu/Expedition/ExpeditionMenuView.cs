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

        [Inject] private readonly LocalizationService localizationController;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private StarButtonView _star;
        
        [Space]
        [SerializeField] private UnitsSectionView _units;
        [SerializeField] private LocationsSectionView _locations;
        [SerializeField] private SidebarView _sidebar;
        [SerializeField] private StartButtonView _start;
        
        public UnitsSectionView Units => _units;

        #endregion

        #region Variables

        public LocationObject ActiveLocation { get; set; }
        public int ActiveStar { get; set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _units.EventClick += OnUnitClick;
            _locations.EventClick += OnLocationClick;
            _star.EventClick += OnStarClick;
            _start.EventClick += Close;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _units.EventClick -= OnUnitClick;
            _locations.EventClick -= OnLocationClick;
            _star.EventClick -= OnStarClick;
            _start.EventClick -= Close;
        }

        #endregion

        #region Click Handlers

        private void OnUnitClick()
        {
            _sidebar.SetData();
            _star.ResetStar();
            _start.SetState();
        }
        
        private void OnStarClick()
        {
            _sidebar.SetData();
        }
        
        private void OnLocationClick()
        {
            _sidebar.SetData();
            _star.ResetStar();
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            _star.Initialize();
            _units.Initialize();
            _locations.Initialize();
            _sidebar.Initialize();
            _start.Initialize();
            
            _title.text = localizationController.GetLanguageValue(LocalizationKeys.UnitsMenuTitleKey);
        }
    }
}
