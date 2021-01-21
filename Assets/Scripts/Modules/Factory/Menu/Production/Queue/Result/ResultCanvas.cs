using Modules.General.Item.Production.Models.Object;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Queue.Result
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Queue/Result Canvas")]
    public class ResultCanvas : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
            GetComponent<Canvas>().planeDistance = 100;
        }

        public class Factory : PlaceholderFactory<ProductionObject, ResultCanvas> { }
    }
}
