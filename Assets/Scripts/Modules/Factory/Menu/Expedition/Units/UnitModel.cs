using System.Linq;
using Modules.General.Unit.Models.Object;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Units
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Unit Model")]
    public class UnitModel : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ExpeditionMenuManager _expeditionMenuManager;

        #endregion
        
        #region Variables

        private UnitObject _data;
        
        private Transform _unitTransform;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _unitTransform = transform;

            _unitTransform.localPosition = new Vector3(0, -150, -150);
            _unitTransform.localRotation = Quaternion.Euler(0, 135, 0);
            _unitTransform.localScale = new Vector3(200, 200, 200);
        }

        #endregion

        public void SetData(UnitObject data)
        {
            _data = data;
        }
        
        private void OnMouseDown()
        {
            var activeUnit = _expeditionMenuManager.Units.GetUnitsWithData().First(x => x.Data == _data);
            if (activeUnit.Data != null)
            {
                activeUnit.ActivateCell(true);
                activeUnit.ResetData();
                
                _expeditionMenuManager.Units.OnClickEvent?.Invoke();
                
                Destroy(gameObject);
            }
        }
    }
}
