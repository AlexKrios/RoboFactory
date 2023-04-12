using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using JetBrains.Annotations;
using Modules.Factory.Building.Floors;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Cameras
{
    [UsedImplicitly]
    public class FactoryCameraController
    {
        #region Zenject
        
        [Inject(Id = "MainCameraParent")] private readonly Transform _mainCamera;

        #endregion

        #region Variables

        public Dictionary<int, BuildingFloorBase> FloorsDictionary { get; }

        private BuildingFloorBase _activeFloor;
        private BuildingFloorBase ActiveFloor
        {
            get => _activeFloor;
            set
            {
                if (_activeFloor != null)
                    _activeFloor.SetFloorVisible(false);
                
                _activeFloor = value;
                FloorsDictionary.Values.ToList()
                    .ForEach(x => x.SetFloorVisible(_activeFloor.FloorNumber >= x.FloorNumber));
                
                _activeFloor.SetFloorActive();
                _activeFloor.SetMenuButtonsActive();
            }
        }
        
        private Sequence _camAnim;
        private float _targetPosY;

        #endregion

        public FactoryCameraController()
        {
            FloorsDictionary = new Dictionary<int, BuildingFloorBase>();
        }

        public void Init()
        {
            FloorsDictionary.Values.ToList().ForEach(x => x.SetFloorVisible(false));
            ActiveFloor = FloorsDictionary[1];
        }

        public void Move(int direction)
        {
            var floorNumber = ActiveFloor.FloorNumber;
            if (floorNumber >= FloorsDictionary.Count && direction == 1 || floorNumber <= 1 && direction == -1)
                return;
            
            ActiveFloor = FloorsDictionary[floorNumber + direction];
            StartMoveAnimation();
        }

        private void StartMoveAnimation()
        {
            _camAnim?.Kill();
            _camAnim = DOTween.Sequence();
            _camAnim.Append(_mainCamera.DOMoveY(_activeFloor.CameraPoint.position.y, 0.5f)
                .SetEase(Ease.OutCubic));
        }
    }
}