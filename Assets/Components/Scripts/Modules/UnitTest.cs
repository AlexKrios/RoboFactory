using UnityEngine;

namespace RoboFactory
{
    public class UnitTest : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _cameraTarget;

        private void Update()
        {
            _mainCamera.transform.RotateAround(_cameraTarget.position, Vector3.up, 15 * Time.deltaTime);
            //mainCamera.transform.LookAt(cameraTarget);
        }
    }
}