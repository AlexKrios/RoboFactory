using UnityEngine;

namespace Modules.Factory.Cameras.View
{
    [AddComponentMenu("Scripts/Factory/Cameras/Camera Point View")]
    public class CameraPointView : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}