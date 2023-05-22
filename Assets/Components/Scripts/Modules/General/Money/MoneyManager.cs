using System;
using Components.Scripts.Modules.Factory.Api;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Zenject;

namespace Components.Scripts.Modules.General.Money
{
    [UsedImplicitly]
    public class MoneyManager
    {
        [Inject] private readonly ApiManager _apiManager;
        
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
            await _apiManager.SetUserMoney(moneyObject);
        }
    }
}