using RoboFactory.Battle;
using RoboFactory.Battle.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    [AddComponentMenu("Scripts/Factory/Di/Battle Installer")]
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