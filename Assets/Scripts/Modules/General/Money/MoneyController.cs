using System;
using Modules.General.Money.Models;
using Modules.General.Save;
using Zenject;

namespace Modules.General.Money
{
    public class MoneyController : IMoneyController
    {
        [Inject] private readonly ISaveController _saveController;
        
        public Action OnMoneySet { get; set; }

        public int Money { get; private set; }

        public void LoadData(MoneyObject obj)
        {
            Money = obj.money;
            OnMoneySet?.Invoke();
        }
        
        public void SetMoney(int money)
        {
            Money = money;
            OnMoneySet?.Invoke();
            _saveController.SaveMoney();
        }

        public void PlusMoney(int money)
        {
            Money += money;
            OnMoneySet?.Invoke();
            _saveController.SaveMoney();
        }
        
        public void MinusMoney(int money)
        {
            Money -= money;
            OnMoneySet?.Invoke();
            _saveController.SaveMoney();
        }
    }
}