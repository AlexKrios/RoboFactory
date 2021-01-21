using System;
using Modules.General.Money.Models;

namespace Modules.General.Money
{
    public interface IMoneyController
    {
        Action OnMoneySet { get; set; }
        
        int Money { get; }
        
        void LoadData(MoneyObject obj);

        void SetMoney(int money);
        void PlusMoney(int money);
        void MinusMoney(int money);
    }
}
