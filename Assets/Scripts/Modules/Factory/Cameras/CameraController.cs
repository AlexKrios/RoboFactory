using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Factory.Building;
using Modules.Factory.Building.Floors;
using Modules.General.Coroutines;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Cameras
{
    [UsedImplicitly]
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
        
        private CompositeDisposable _disposable;

        #endregion

        public void Init()
        {
            _disposable = new CompositeDisposable();
            
            MoveUp.OnClickAsObservable().Subscribe(_ => Move(CameraDirection.Up)).AddTo(_disposable);
            MoveDown.OnClickAsObservable().Subscribe(_ => Move(CameraDirection.Down)).AddTo(_disposable);

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