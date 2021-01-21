using UnityEngine;

namespace Modules
{
    public class UnitTest : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform cameraTarget;

        private void Update()
        {
            mainCamera.transform.RotateAround(cameraTarget.position, Vector3.up, 5 * Time.deltaTime);
            //mainCamera.transform.LookAt(cameraTarget);
        }
    }
}