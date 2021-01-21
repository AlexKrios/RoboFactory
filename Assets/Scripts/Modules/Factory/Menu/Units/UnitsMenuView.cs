using Modules.Factory.Menu.Units.Header;
using Modules.Factory.Menu.Units.Info;
using Modules.Factory.Menu.Units.Roster;
using Modules.Factory.Menu.Units.Sidebar;
using Modules.Factory.Menu.Units.Tab;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Units Menu View")]
    public class UnitsMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly UnitsMenuManager _unitsMenuManager;

        #endregion
        
        #region Components
        
        [SerializeField] private HeaderView header;
        [SerializeField] private TabsSectionView tabs;
        [SerializeField] private RosterSectionView roster;
        [SerializeField] private InfoView info;
        [SerializeField] private SidebarView sidebar;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
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
            _unitsMenuManager.Reset();
        }
    }
}
