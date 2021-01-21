using Modules.General.Item.Raw;
using Modules.General.Level;
using Modules.General.Money;
using Modules.General.Order;
using Modules.General.Save;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.General.Ui.Admin
{
    public class DebugMenuView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly ILevelController _levelController;
        [Inject] private readonly IRawController _rawController;
        [Inject] private readonly IOrderController _orderController;

        #endregion
        
        #region Components
        
        [Header("Money")]
        [SerializeField] private Button moneyPlus;
        [SerializeField] private Button bankrupt;

        [Header("Experience")]
        [SerializeField] private Button experiencePlus;
        [SerializeField] private Button experiencePlusDouble;

        [Header("Raw")]
        [SerializeField] private Button rawMax;
        [SerializeField] private Button rawMin;
        
        [Space]
        [SerializeField] private Button resetOrder;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            moneyPlus.onClick.AddListener(OnMoneyPlus);
            bankrupt.onClick.AddListener(OnBankrupt);
            experiencePlus.onClick.AddListener(OnExperiencePlus);
            experiencePlusDouble.onClick.AddListener(OnExperiencePlusDouble);
            rawMax.onClick.AddListener(OnRawMax);
            rawMin.onClick.AddListener(OnRawMin);
            resetOrder.onClick.AddListener(OnResetOrder);
        }
        
        private void OnDestroy()
        {
            moneyPlus.onClick.RemoveListener(OnMoneyPlus);
            bankrupt.onClick.RemoveListener(OnBankrupt);
            experiencePlus.onClick.RemoveListener(OnExperiencePlus);
            experiencePlusDouble.onClick.RemoveListener(OnExperiencePlusDouble);
            rawMax.onClick.RemoveListener(OnRawMax);
            rawMin.onClick.RemoveListener(OnRawMin);
            resetOrder.onClick.RemoveListener(OnResetOrder);
        }

        #endregion
        
        private void OnMoneyPlus()
        {
            _moneyController.PlusMoney(1000);
        }
        
        private void OnBankrupt()
        {
            _moneyController.SetMoney(0);
        }
        
        private void OnExperiencePlusDouble()
        {
            _levelController.SetExperience(1000);
        }

        private void OnExperiencePlus()
        {
            _levelController.SetExperience(50);
        }

        private void OnRawMax()
        {
            _rawController.SetMaxRaw();
            _saveController.Save();
        }
        
        private void OnRawMin()
        {
            _rawController.SetMinRaw();
            _saveController.Save();
        }
        
        private void OnResetOrder()
        {
            _orderController.RefreshOrdersForce();
            _saveController.SaveOrders();
        }
    }
}
