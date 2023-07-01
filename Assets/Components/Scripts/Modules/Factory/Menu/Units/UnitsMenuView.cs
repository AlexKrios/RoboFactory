using RoboFactory.Factory.Menu.Units;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Units Menu View")]
    public class UnitsMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly AssetsManager _assetsManager;

        #endregion
        
        #region Components
        
        [Space]
        [SerializeField] private HeaderView header;
        [SerializeField] private TabsSectionView tabs;
        [SerializeField] private RosterSectionView roster;
        [SerializeField] private InfoView info;
        [SerializeField] private SidebarView sidebar;
        
        public InfoView Info => info;

        #endregion

        #region Variables

        public UnitType ActiveUnitType { get; set; }
        public UnitObject ActiveUnit { get; set; }
        public ProductObject ActiveEquipment { get; set; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            tabs.OnTabClickEvent += OnTabClick;
            roster.OnUnitClickEvent += OnUnitClick;
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            header.Initialize();
            tabs.Initialize();
            roster.Initialize();
            info.Initialize();
            sidebar.Initialize();
        }

        private void OnTabClick()
        {
            header.SetData();
            roster.CreateUnits();
            info.SetData();
            sidebar.SetData();
        }
        
        private void OnUnitClick()
        {
            info.SetData();
            sidebar.SetData();
        }
        
        public override void Close()
        {
            base.Close();
         
            info.gameObject.SetActive(false);
        }
        
        protected override void Release()
        {
            _assetsManager.ReleaseAllAsset();
        }
    }
}
