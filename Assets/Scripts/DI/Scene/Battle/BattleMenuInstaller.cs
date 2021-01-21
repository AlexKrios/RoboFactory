using Modules.Battle;
using Modules.Battle.Ui;
using Modules.Battle.Ui.End;
using UnityEngine;
using Zenject;

namespace Di.Scene.Battle
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