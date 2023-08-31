using System.Collections.Generic;
using JetBrains.Annotations;
using RoboFactory.Factory.Cameras;
using RoboFactory.Factory.Menu;
using RoboFactory.General.Ui;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace RoboFactory.Factory.Building
{
    public class BuildingFloorBase : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly FactoryCameraController _factoryCameraController;
        [Inject] private readonly MenuButtonsView _menuButtonsView;

        #endregion

        #region Components

        [SerializeField] private int _floorNumber;
        [SerializeField] private List<UiType> _menuButtons;
        
        [Header("Links")]
        [SerializeField] private Transform _cameraPoint;

        [Header("Walls")]
        [SerializeField, UsedImplicitly] private MeshRenderer _ground;
        [SerializeField, UsedImplicitly] private MeshRenderer _roof;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> _leftWall;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> _rightWall;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> _frontWall;
        [SerializeField, UsedImplicitly] private List<MeshRenderer> _backWall;

        [Header("Columns")]
        [SerializeField, UsedImplicitly] private MeshRenderer _columnFrontLeft;
        [SerializeField, UsedImplicitly] private MeshRenderer _columnFrontRight;
        [SerializeField, UsedImplicitly] private MeshRenderer _columnBackLeft;
        [SerializeField, UsedImplicitly] private MeshRenderer _columnBackRight;
        
        public int FloorNumber => _floorNumber;
        public Transform CameraPoint => _cameraPoint;

        #endregion

        private void Awake()
        {
            _factoryCameraController.FloorsDictionary.Add(_floorNumber, this);
        }

        public void SetFloorActive()
        {
            SetFloorVisible(true);

            _leftWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.ShadowsOnly);
            _frontWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.ShadowsOnly);
            if (_roof != null)
                _roof.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            if (_columnFrontLeft != null)
                _columnFrontLeft.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }

        public void SetFloorVisible(bool value)
        {
            gameObject.SetActive(value);
            
            _leftWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.On);
            _frontWall.ForEach(x => x.shadowCastingMode = ShadowCastingMode.On);
            if (_roof != null)
                _roof.shadowCastingMode = ShadowCastingMode.On;
            if (_columnFrontLeft != null)
                _columnFrontLeft.shadowCastingMode = ShadowCastingMode.On;
        }

        public void SetMenuButtonsActive()
        {
            _menuButtonsView.SetMenuButtonsActive(false);
            _menuButtonsView.SetMenuButtonsActive(true, _menuButtons);
        }
    }
}