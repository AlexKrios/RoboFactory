using RoboFactory.Factory.Cameras;
using RoboFactory.General.Audio;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory
{
    public class FactoryMediator : MonoBehaviour
    {
        [Inject] private readonly AudioService _audioService;
        [Inject] private readonly FactoryCameraController _factoryCameraController;

        private void Start()
        {
            _factoryCameraController.Init();
            _audioService.PlayMusic();
        }
    }
}