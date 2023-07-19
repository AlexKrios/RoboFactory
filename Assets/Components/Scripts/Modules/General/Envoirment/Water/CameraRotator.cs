using UnityEngine;

namespace Assets.MobileOptimizedWater.Scripts
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private Transform transformComponent;

        private void Awake()
        {
            transformComponent = GetComponent<Transform>();
        }

        public void Update()
        {
            var angles = transformComponent.eulerAngles;
            angles.y += Time.deltaTime * _speed;

            transformComponent.eulerAngles = angles;
        }
    }
}
