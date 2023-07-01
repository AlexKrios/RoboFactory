using IngameDebugConsole;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    [AddComponentMenu("Scripts/General/Di/Debug Installer")]
    public class DebugInstaller : MonoInstaller
    {
        private const string DebugRoot = "Debug";
        
        public override void InstallBindings()
        {
            Container.Bind<DebugLogManager>()
                .FromComponentInNewPrefabResource(DebugRoot)
                .UnderTransform(transform)
                .AsSingle()
                .NonLazy();
        }
    }
}