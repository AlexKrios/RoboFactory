using RoboFactory.Battle.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    [CreateAssetMenu(menuName = "Scriptable/Battle/Settings", order = 2)]
    public class BattleSettingsInstaller : ScriptableObjectInstaller<BattleSettingsInstaller>
    {
        public BattleUiFactory.Settings battleUi;
        public EndBattleFactory.Settings endBattle;
        
        public override void InstallBindings()
        {
            Container.BindInstance(battleUi);
            Container.BindInstance(endBattle);
        }
    }
}