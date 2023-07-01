using RoboFactory.General.Asset;
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

        [Inject] private readonly AssetsManager _assetsManager;
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

            units.EventClick += OnUnitClick;
            locations.EventClick += OnLocationClick;
            start.EventClick += Close;
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

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            star.Initialize();
            units.Initialize();
            locations.Initialize();
            sidebar.Initialize();
            start.Initialize();
            
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.UnitsMenuTitleKey);
        }
        
        protected override void Release()
        {
            _assetsManager.ReleaseAllAsset();
        }
    }
}
