using RoboFactory.Factory.Menu.Units;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;

namespace RoboFactory.Factory.Menu
{
    public class UnitsMenuView : MenuBase
    {
        [Space]
        [SerializeField] private HeaderView _header;
        [SerializeField] private TabsSectionView _tabs;
        [SerializeField] private RosterSectionView _roster;
        [SerializeField] private InfoView _info;
        [SerializeField] private SidebarView _sidebar;
        
        public InfoView Info => _info;

        public UnitType ActiveUnitType { get; set; }
        public UnitObject ActiveUnit { get; set; }
        public ProductObject ActiveEquipment { get; set; }

        protected override void Awake()
        {
            base.Awake();

            _tabs.OnTabClickEvent += OnTabClick;
            _roster.OnUnitClickEvent += OnUnitClick;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            _header.Initialize();
            _tabs.Initialize();
            _roster.Initialize();
            _info.Initialize();
            _sidebar.Initialize();
        }

        private void OnTabClick()
        {
            _header.SetData();
            _roster.CreateUnits();
            _info.SetData();
            _sidebar.SetData();
        }
        
        private void OnUnitClick()
        {
            _info.SetData();
            _sidebar.SetData();
        }
        
        public override void Close()
        {
            base.Close();
         
            _info.gameObject.SetActive(false);
        }
    }
}
