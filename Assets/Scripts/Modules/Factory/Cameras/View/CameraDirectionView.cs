using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Cameras.View
{
    [AddComponentMenu("Scripts/Factory/Cameras/Camera Direction View")]
    public class CameraDirectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly CameraController _cameraController;

        #endregion

        #region Variables

        [SerializeField] private CameraDirection direction;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            var buttonComponent = GetComponent<Button>();
            
            switch (direction)
            {
                case CameraDirection.Up:
                    _cameraController.MoveUp = buttonComponent;
                    break;
                
                case CameraDirection.Down:
                    _cameraController.MoveDown = buttonComponent;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}