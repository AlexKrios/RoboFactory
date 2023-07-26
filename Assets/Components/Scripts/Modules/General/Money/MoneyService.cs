using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Profile;
using RoboFactory.General.Services;
using UniRx;
using Zenject;

namespace RoboFactory.General.Money
{
    [UsedImplicitly]
    public class MoneyService : Service
    {
        protected override string InitializeTextKey => "initialize_1";
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;
        
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;

        public IntReactiveProperty Money { get; } = new();

        protected override UniTask InitializeAsync()
        {
            var data = _commonProfile.UserProfile.MoneySection;
            
            Money.Value = data.Money;

            return UniTask.CompletedTask;
        }
        
        public async void SetMoney(int money)
        {
            await SendMoneyData(money);
            
            Money.Value = money;
        }

        public async void PlusMoney(int money)
        {
            await SendMoneyData(Money.Value + money);
            
            Money.Value += money;
        }
        
        public async UniTask MinusMoney(int money)
        {
            await SendMoneyData(Money.Value - money);
            
            Money.Value -= money;
        }

        private async UniTask SendMoneyData(int newMoney)
        {
            var moneyObject = new MoneyObject { Money = newMoney };
            await _apiService.SetUserMoney(moneyObject);
        }
    }
}