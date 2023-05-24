using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Location;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Locations Section View")]
    public class LocationsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly LocationManager _locationManager;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;

        #endregion

        #region Components

        [SerializeField] private RectTransform unitParent;
        [SerializeField] private List<LocationCellView> locations;

        #endregion

        #region Variables

        public Action EventClick { get; set; }

        private ExpeditionMenuView _menu;
        private LocationCellView _activeLocation;
        private LocationCellView ActiveLocation
        {
            get => _activeLocation;
            set
            {
                if (_activeLocation != null)
                    _activeLocation.SetInactive();

                _activeLocation = value;
                _menu.ActiveLocation = value.Data;
                _activeLocation.SetActive();
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            CreateLocations();
        }

        #endregion

        private void CreateLocations()
        {
            if (locations.Count != 0)
                RemoveLocations();
            
            foreach (var location in _locationManager.GetLocations())
            {
                var cell = _expeditionMenuFactory.CreateLocationCell(unitParent);
                cell.OnClickEvent += OnLocationClick;
                cell.SetData(location);
                locations.Add(cell);
            }

            var width = 240 * locations.Count + 10 * (locations.Count - 1);
            unitParent.sizeDelta = new Vector2(width, 240);

            ActiveLocation = locations.First();
        }
        
        private void RemoveLocations()
        {
            locations.ForEach(x => Destroy(x.gameObject));
            locations.Clear();
        }

        private void OnLocationClick(LocationCellView location)
        {
            if (ActiveLocation == location)
                return;

            ActiveLocation = location;
            
            EventClick?.Invoke();
        }
    }
}