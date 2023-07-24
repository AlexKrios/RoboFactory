namespace Assets.MobileOptimizedWater.Scripts
{
    using UnityEngine;

    public class TouchRotator : MonoBehaviour
    {
        [SerializeField] private Transform _cameraRoot;
        [SerializeField] private float _mouseSpeed;

        private Vector3 prevMousePos;

#if !UNITY_EDITOR
        public void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width * 0.9f)
            {
                var deltaPos = Input.GetTouch(0).deltaPosition;

                var deltaRotation = new Vector3(-deltaPos.y, deltaPos.x) * Time.deltaTime;
                var rotation = _cameraRoot.eulerAngles + deltaRotation;

                _cameraRoot.eulerAngles = new Vector3(Mathf.Clamp(rotation.x, 0f, 90f), rotation.y);
            }
        }
#else
        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                var deltaPos = Input.mousePosition - prevMousePos;

                var deltaRotation = new Vector3(-deltaPos.y, deltaPos.x) * (Time.deltaTime * _mouseSpeed);
                var rotation = _cameraRoot.eulerAngles + deltaRotation;

                _cameraRoot.eulerAngles = new Vector3(Mathf.Clamp(rotation.x, 0f, 90f), rotation.y);
            }

            prevMousePos = Input.mousePosition;
        }
#endif
    }
}
