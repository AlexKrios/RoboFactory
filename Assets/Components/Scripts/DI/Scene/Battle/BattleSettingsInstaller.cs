using Components.Scripts.Modules.Battle.Ui;
using Components.Scripts.Modules.Battle.Ui.End;
using UnityEngine;
using Zenject;

namespace Components.Scripts.DI.Scene.Battle
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