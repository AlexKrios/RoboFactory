using System.Collections;
using Components.Scripts.Modules.General.Level;
using TMPro;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.General.Ui.Admin
{
    [AddComponentMenu("Scripts/General/Ui/Admin/FpsCounterView")]
    public class FpsCounterView : MonoBehaviour
    {
        [Inject] private readonly LevelManager levelManager;
        
        [SerializeField] private TMP_Text fps;

        private void Awake()
        {
            StartCoroutine(UpdateCounter());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                levelManager.SetExperience(10);
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