using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using Zenject;

namespace RoboFactory.General.Money
{
    [UsedImplicitly]
    public class MoneyManager
    {
        [Inject] private readonly ApiService apiService;
        
        public Action OnMoneySet { get; set; }

        public int Money { get; private set; }

        public void LoadData(MoneyObject obj)
        {
            Money = obj.money;
            OnMoneySet?.Invoke();
        }
        
        public async void SetMoney(int money)
        {
            await SendMoneyData(money);
            
            Money = money;
            OnMoneySet?.Invoke();
        }

        public async void PlusMoney(int money)
        {
            await SendMoneyData(Money + money);
            
            Money += money;
            OnMoneySet?.Invoke();
        }
        
        public async UniTask MinusMoney(int money)
        {
            await SendMoneyData(Money - money);
            
            Money -= money;
            OnMoneySet?.Invoke();
        }

        private async UniTask SendMoneyData(int newMoney)
        {
            var moneyObject = new MoneyObject { money = newMoney };
            await apiService.SetUserMoney(moneyObject);
        }
    }
}