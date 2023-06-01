using UnityEngine;

namespace Assets.MobileOptimizedWater.Scripts
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private Transform transformComponent;

        private void Awake()
        {
            transformComponent = GetComponent<Transform>();
        }

        public void Update()
        {
            var angles = transformComponent.eulerAngles;
            angles.y += Time.deltaTime * speed;

            transformComponent.eulerAngles = angles;
        }
    }
}
