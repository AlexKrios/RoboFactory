using Components.Scripts.Modules.Factory.Menu.Units.Header;
using Components.Scripts.Modules.Factory.Menu.Units.Info;
using Components.Scripts.Modules.Factory.Menu.Units.Roster;
using Components.Scripts.Modules.Factory.Menu.Units.Sidebar;
using Components.Scripts.Modules.Factory.Menu.Units.Tab;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Modules.General.Unit.Object;
using Components.Scripts.Modules.General.Unit.Type;
using UnityEngine;

namespace Components.Scripts.Modules.Factory.Menu.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Units Menu View")]
    public class UnitsMenuView : MenuBase
    {
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
            
            UiController.AddUi(this);
            
            tabs.OnTabClickEvent += OnTabClick;
            roster.OnUnitClickEvent += OnUnitClick;
        }

        #endregion
        
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
    }
}
