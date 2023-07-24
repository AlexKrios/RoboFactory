using Cysharp.Threading.Tasks;
using RoboFactory.General.Level;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Ui.Admin
{
    public class FpsCounterView : MonoBehaviour
    {
        [Inject] private readonly LevelManager levelManager;
        
        [SerializeField] private TMP_Text _fps;

        private void Awake()
        {
            UpdateCounter();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                levelManager.SetExperience(500);
        }

        private async void UpdateCounter()
        {
            while (true)
            {
                _fps.text = $"{(int)(1f / Time.unscaledDeltaTime)}fps";

                await UniTask.Delay(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}