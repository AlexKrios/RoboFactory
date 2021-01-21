using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units.Info
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("Scripts/Factory/Menu/Units/Unit Model")]
    public class UnitModel : MonoBehaviour
    {
        #region Zenject

        [Inject(Id = "MenuCamera")] private readonly Camera _menuCamera;

        #endregion
        
        #region Variables

        private Transform _unitTransform;
        private Rigidbody _unitRigidbody;

        private float _startPosition;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _unitTransform = transform;
            _unitRigidbody = GetComponent<Rigidbody>();
            
            _unitTransform.localPosition = new Vector3(325, -500, -150);
            _unitTransform.localRotation = Quaternion.Euler(0, 180, 0);
            _unitTransform.localScale = new Vector3(250, 250, 250);

            _unitRigidbody.constraints = (RigidbodyConstraints) 94;
        }

        private void FixedUpdate()
        {
            #if UNITY_EDITOR && UNITY_ANDROID
            RotateEditor();
            #elif UNITY_ANDROID
            RotateMobile();
            #endif
        }

        private void RotateEditor()
        {
            var turn = Input.GetAxis("Horizontal");
            _unitRigidbody.AddRelativeTorque(Vector3.down * (10 * turn));
        }
        
        [UsedImplicitly]
        private void RotateMobile()
        {
            if (Input.touchCount != 1)
                return;

            var touch = Input.GetTouch(0);
            var ray = _menuCamera.ScreenPointToRay(touch.position);
            if(Physics.Raycast(ray, out var hit))
            {
                if(hit.transform.gameObject != transform.parent.gameObject) 
                    return;
            }
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPosition = touch.position.x;
                    break;
                
                case TouchPhase.Moved:
                    if (_startPosition > touch.position.x)
                        _unitRigidbody.AddRelativeTorque(Vector3.up * 10);
                    else if (_startPosition < touch.position.x)
                        _unitRigidbody.AddRelativeTorque(Vector3.down * 10);
                    break;
            }
        }

        #endregion
    }
}
