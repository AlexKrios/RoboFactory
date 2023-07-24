using RoboFactory.Battle;
using RoboFactory.Battle.Ui;
using Zenject;

namespace RoboFactory.DI
{
    public class BattleMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BattleController>().AsSingle().NonLazy();
            Container.Bind<BattleUiFactory>().AsSingle().NonLazy();
            Container.Bind<EndBattleFactory>().AsSingle().NonLazy();
        }
    }
}