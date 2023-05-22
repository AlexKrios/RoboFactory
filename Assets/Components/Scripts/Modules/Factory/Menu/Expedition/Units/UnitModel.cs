using System.Linq;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Unit.Object;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Expedition.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Unit Model")]
    public class UnitModel : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;

        #endregion
        
        #region Variables

        private ExpeditionMenuView _menu;
        
        private UnitObject _data;
        
        private Transform _unitTransform;

        #endregion

        #region Unity Methods

        private async void Awake()
        {
            await UniTask.WaitUntil(() => _uiController != null);
            _menu = _uiController.FindUi<ExpeditionMenuView>();

            _unitTransform = transform;

            _unitTransform.localPosition = new Vector3(0, 0, -250);
            _unitTransform.localRotation = Quaternion.Euler(0, 135, 0);
            _unitTransform.localScale = new Vector3(175, 175, 200);
        }

        #endregion

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
