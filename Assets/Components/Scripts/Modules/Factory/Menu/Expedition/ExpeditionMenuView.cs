using RoboFactory.General.Localization;
using RoboFactory.General.Location;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class ExpeditionMenuView : MenuBase
    {
        [Inject] private readonly LocalizationService _localizationService;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private StarButtonView _star;
        
        [Space]
        [SerializeField] private UnitsSectionView _units;
        [SerializeField] private LocationsSectionView _locations;
        [SerializeField] private SidebarView _sidebar;
        [SerializeField] private StartButtonView _start;
        
        public UnitsSectionView Units => _units;

        public LocationObject ActiveLocation { get; set; }
        public int ActiveStar { get; set; }
        
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

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            _star.Initialize();
            _units.Initialize();
            _locations.Initialize();
            _sidebar.Initialize();
            _start.Initialize();
            
            _title.text = _localizationService.GetLanguageValue(LocalizationKeys.UnitsMenuTitleKey);
        }
    }
}
