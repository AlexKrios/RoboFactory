using System.Linq;
using Cysharp.Threading.Tasks;
using RoboFactory.General.Ui;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class UnitModel : MonoBehaviour
    {
        [Inject] private readonly IUiController _uiController;
        
        private Transform _unitTransform;
        
        private ExpeditionMenuView _menu;
        private UnitObject _data;

        private async void Awake()
        {
            //TODO !!!
            await UniTask.WaitUntil(() => _uiController != null);
            _menu = _uiController.FindUi<ExpeditionMenuView>();

            _unitTransform = transform;

            _unitTransform.localPosition = new Vector3(0, 0, -250);
            _unitTransform.localRotation = Quaternion.Euler(0, 135, 0);
            _unitTransform.localScale = new Vector3(175, 175, 200);
        }

        public void SetData(UnitObject data)
        {
            _data = data;
        }
        
        private void OnMouseDown()
        {
            var activeUnit = _menu.Units.GetUnitsWithData().First(x => x.Data == _data);
            if (activeUnit.Data != null)
            {
                activeUnit.ActivateCell(true);
                activeUnit.ResetData();
                
                _menu.Units.EventClick?.Invoke();
                
                Destroy(gameObject);
            }
        }
    }
}
