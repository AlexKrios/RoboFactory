using Components.Scripts.Modules.Battle;
using Components.Scripts.Modules.Battle.Ui;
using Components.Scripts.Modules.Battle.Ui.End;
using UnityEngine;
using Zenject;

namespace Components.Scripts.DI.Scene.Battle
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