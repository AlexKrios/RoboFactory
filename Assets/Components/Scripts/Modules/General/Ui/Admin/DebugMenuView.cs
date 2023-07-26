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

        [Inject] private readonly MoneyService _moneyService;
        [Inject] private readonly ExperienceService _experienceService;
        [Inject] private readonly RawService _rawService;
        [Inject] private readonly OrderService orderService;

        #endregion
        
        #region Components
        
        [Header("Money")]
        [SerializeField] private Button _moneyPlus;
        [SerializeField] private Button _bankrupt;

        [Header("Experience")]
        [SerializeField] private Button _experiencePlus;
        [SerializeField] private Button _experiencePlusDouble;

        [Header("Raw")]
        [SerializeField] private Button _rawMax;
        [SerializeField] private Button _rawMin;
        
        [Space]
        [SerializeField] private Button _resetOrder;

        #endregion

        #region Variables

        private readonly CompositeDisposable _disposable = new();

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _moneyPlus.OnClickAsObservable().Subscribe(_ => OnMoneyPlus()).AddTo(_disposable);
            _bankrupt.OnClickAsObservable().Subscribe(_ => OnBankrupt()).AddTo(_disposable);
            _experiencePlus.OnClickAsObservable().Subscribe(_ => OnExperiencePlus()).AddTo(_disposable);
            _experiencePlusDouble.OnClickAsObservable().Subscribe(_ => OnExperiencePlusDouble()).AddTo(_disposable);
            _rawMax.OnClickAsObservable().Subscribe(_ => OnRawMax()).AddTo(_disposable);
            _rawMin.OnClickAsObservable().Subscribe(_ => OnRawMin()).AddTo(_disposable);
            _resetOrder.OnClickAsObservable().Subscribe(_ => OnResetOrder()).AddTo(_disposable);
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion
        
        private void OnMoneyPlus()
        {
            _moneyService.PlusMoney(1000);
        }
        
        private void OnBankrupt()
        {
            _moneyService.SetMoney(0);
        }
        
        private void OnExperiencePlusDouble()
        {
            _experienceService.SetExperience(1000);
        }

        private void OnExperiencePlus()
        {
            _experienceService.SetExperience(50);
        }

        private void OnRawMax()
        {
            _rawService.SetMaxRaw();
        }
        
        private void OnRawMin()
        {
            _rawService.SetMinRaw();
        }
        
        private void OnResetOrder()
        {
            orderService.RefreshOrdersForce();
        }
    }
}
