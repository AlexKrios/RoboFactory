using Components.Scripts.Modules.General.Ui.Popup;
using UnityEngine;
using Zenject;

namespace Components.Scripts.DI.Game.Popup
{
    [CreateAssetMenu(menuName = "Scriptable/Popup/Settings", order = 3)]
    public class PopupSettingsInstaller : ScriptableObjectInstaller<PopupSettingsInstaller>
    {
        public PopupFactory.Settings popup;

        public override void InstallBindings()
        {
            Container.BindInstance(popup);
        }
    }
}