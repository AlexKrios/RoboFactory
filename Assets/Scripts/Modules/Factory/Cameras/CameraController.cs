using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Factory.Building;
using Modules.Factory.Building.Floors;
using Modules.General.Coroutines;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Cameras
{
    public class CameraController
    {
        #region Zenject

        [Inject] private readonly ICoroutinesController _coroutinesController;
        [Inject] private readonly IFactoryBuildingController _factoryBuildingController;
        [Inject(Id = "MainCameraParent")] private readonly Transform _mainCamera;

        #endregion

        #region Variables

        public Action OnButtonClick { get; set; }
        
        private Dictionary<int, BuildingFloorBase> FloorsDictionary => _factoryBuildingController.FloorsDictionary;
        private BuildingFloorBase ActiveFloor
        {
            get => _factoryBuildingController.ActiveFloor;
            set => _factoryBuildingController.ActiveFloor = value;
        }

        public Button MoveUp { get; set; }
        public Button MoveDown { get; set; }

        private Vector3 _targetPosition;
        private Coroutine _activeCoroutine;

        #endregion

        public void Init()
        {
            MoveUp.onClick.AddListener(() => Move(CameraDirection.Up));
            MoveDown.onClick.AddListener(() => Move(CameraDirection.Down));

            OnButtonClick += _factoryBuildingController.SetBuildingVisibility;
        }

        private void Move(CameraDirection direction)
        {
            var floorNumber = ActiveFloor.Floor;
            switch (direction)
            {
                case CameraDirection.Up:
                    if (floorNumber >= FloorsDictionary.Count - 1) 
                        return;
                    
                    ActiveFloor = FloorsDictionary[floorNumber + 1];
                    break;
                
                case CameraDirection.Down:
                    if (floorNumber <= 0) 
                        return;
                    
                    ActiveFloor = FloorsDictionary[floorNumber - 1];
                    break;
            }

            StartMoveAnimation();
            OnButtonClick?.Invoke();
        }

        private void StartMoveAnimation()
        {
            _targetPosition = ActiveFloor.CameraPoint.position;
            if (_activeCoroutine != null) 
                return;
            
            _activeCoroutine = _coroutinesController.StartNewCoroutine(MoveAnimation());
        }
        private IEnumerator MoveAnimation()
        {
            var time = 0f;
            while (Vector2.Distance(_mainCamera.position, _targetPosition) > 0.1f)
            {
                yield return new WaitForEndOfFrame();
                _mainCamera.position = Vector3.Lerp(_mainCamera.position, _targetPosition, time);
                time += Time.deltaTime / 5;
            }

            _activeCoroutine = null;
        }
    }
    
    public enum CameraDirection
    {
        Up,
        Down
    }
}