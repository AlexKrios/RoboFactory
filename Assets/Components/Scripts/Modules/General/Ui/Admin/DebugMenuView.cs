using RoboFactory.General.Item.Raw;
using RoboFactory.General.Level;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Ui.Admin
{
    public class DebugMenuView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly OrderManager _orderManager;

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

        #region Variables

        private CompositeDisposable _disposable;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            moneyPlus.OnClickAsObservable().Subscribe(_ => OnMoneyPlus()).AddTo(_disposable);
            bankrupt.OnClickAsObservable().Subscribe(_ => OnBankrupt()).AddTo(_disposable);
            experiencePlus.OnClickAsObservable().Subscribe(_ => OnExperiencePlus()).AddTo(_disposable);
            experiencePlusDouble.OnClickAsObservable().Subscribe(_ => OnExperiencePlusDouble()).AddTo(_disposable);
            rawMax.OnClickAsObservable().Subscribe(_ => OnRawMax()).AddTo(_disposable);
            rawMin.OnClickAsObservable().Subscribe(_ => OnRawMin()).AddTo(_disposable);
            resetOrder.OnClickAsObservable().Subscribe(_ => OnResetOrder()).AddTo(_disposable);
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion
        
        private void OnMoneyPlus()
        {
            _moneyManager.PlusMoney(1000);
        }
        
        private void OnBankrupt()
        {
            _moneyManager.SetMoney(0);
        }
        
        private void OnExperiencePlusDouble()
        {
            _levelManager.SetExperience(1000);
        }

        private void OnExperiencePlus()
        {
            _levelManager.SetExperience(50);
        }

        private void OnRawMax()
        {
            _rawManager.SetMaxRaw();
        }
        
        private void OnRawMin()
        {
            _rawManager.SetMinRaw();
        }
        
        private void OnResetOrder()
        {
            _orderManager.RefreshOrdersForce();
            //_saveController.SaveOrders();
        }
    }
}
