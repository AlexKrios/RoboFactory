using System.Collections;
using Modules.General.Level;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.General.Ui.Admin
{
    [AddComponentMenu("Scripts/General/Ui/Admin/FpsCounterView")]
    public class FpsCounterView : MonoBehaviour
    {
        [Inject] private readonly ILevelController _levelController;
        
        [SerializeField] private TextMeshProUGUI fps;

        private void Awake()
        {
            StartCoroutine(UpdateCounter());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                _levelController.SetExperience(10);
        }

        private IEnumerator UpdateCounter()
        {
            while (true)
            {
                fps.text = $"{(int)(1f / Time.unscaledDeltaTime)}fps";
            
                yield return new WaitForSeconds(0.1f);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}