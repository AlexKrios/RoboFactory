using System.Collections;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Queue.Result
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Queue/Result Model")]
    public class ResultModel : MonoBehaviour
    {
        #region Zenject

        //[Inject] private readonly ResultCanvasFactory.Settings _resultCanvasSettings;

        #endregion

        #region Variables

        private Transform _resultTransform;
        private Rigidbody _resultRigidbody;

        #endregion

        #region Unity Methosd

        private void Awake()
        {
            _resultTransform = GetComponent<RectTransform>();
            _resultRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            StartCoroutine(StartTransformation());
        }

        #endregion

        public void SetInitTransform(Transform parent)
        {
            _resultTransform.SetParent(parent);
            _resultTransform.localPosition = new Vector3(0, 0, -650);
            _resultTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        private IEnumerator StartTransformation()
        {
            var startScale = _resultTransform.localScale;
            var targetScale = new Vector3(250, 250, 250);

            var fadeTime = 0f /*_resultCanvasSettings.fadeTime*/;

            yield return new WaitForSeconds(0.1f);

            _resultRigidbody.AddRelativeTorque(Vector3.down * 10000);

            var elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                _resultTransform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / fadeTime);

                yield return new WaitForEndOfFrame();
            }
        }

        private void FixedUpdate()
        {
            var turn = Input.GetAxis("Horizontal");
            _resultRigidbody.AddRelativeTorque(Vector3.up * (10 * turn));

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                var touch = Input.GetTouch(0);
                var x = touch.deltaPosition.x * 10;

                _resultRigidbody.AddRelativeTorque(Vector3.down * x);
            }
        }

        public class Factory : PlaceholderFactory<GameObject, ResultModel> { }
    }
}
