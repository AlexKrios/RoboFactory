using RoboFactory.Battle.Ui;
using Zenject;

namespace RoboFactory.DI
{
    public class BattleSettingsInstaller : ScriptableObjectInstaller<BattleSettingsInstaller>
    {
        public BattleUiFactory.Settings _battleUi;
        public EndBattleFactory.Settings _endBattle;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_battleUi);
            Container.BindInstance(_endBattle);
        }
    }
}