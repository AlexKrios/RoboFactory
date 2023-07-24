using IngameDebugConsole;
using Zenject;

namespace RoboFactory.DI
{
    public class DebugInstaller : MonoInstaller
    {
        private const string DebugRoot = "Debug";
        
        public override void InstallBindings()
        {
            Container.Bind<DebugLogManager>()
                .FromComponentInNewPrefabResource(DebugRoot)
                .AsSingle()
                .NonLazy();
        }
    }
}