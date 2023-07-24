using RoboFactory.General.Ui.Popup;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    [CreateAssetMenu(menuName = "Scriptable/Popup/Settings", order = 3)]
    public class PopupSettingsInstaller : ScriptableObjectInstaller<PopupSettingsInstaller>
    {
        [SerializeField] private PopupFactory.Settings _popup;

        public override void InstallBindings()
        {
            Container.BindInstance(_popup);
        }
    }
}