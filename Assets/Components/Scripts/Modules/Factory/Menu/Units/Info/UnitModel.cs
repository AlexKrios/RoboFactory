using JetBrains.Annotations;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Units
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("Scripts/Factory/Menu/Units/Unit Model")]
    public class UnitModel : MonoBehaviour
    {
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
            
            _unitTransform.localPosition = new Vector3(250, -300, -250);
            _unitTransform.localRotation = Quaternion.Euler(0, 135, 0);
            _unitTransform.localScale = new Vector3(300, 300, 300);

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
        
        #endregion

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

            var cam = Camera.current;
            var touch = Input.GetTouch(0);
            var ray = cam.ScreenPointToRay(touch.position);
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
    }
}
