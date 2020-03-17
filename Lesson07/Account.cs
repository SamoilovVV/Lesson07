using System;
using System.Collections.Generic;
using System.Text;

namespace UsingDelegates
{
    class Account
    {
        public delegate void AccountStateHandler(string message);

        private AccountStateHandler _stateHandler;

        public Account(int sum)
        {
            CurrentSum = sum;
        }

        public void RegisterStateHandler(AccountStateHandler stateHandler)
        {
            _stateHandler += stateHandler;
        }

        public void UnregisterStateHandler(AccountStateHandler stateHandler)
        {
            _stateHandler -= stateHandler;
        }

        public int CurrentSum { get; private set; }

        public void Put(int sum)
        {
            CurrentSum += sum;
            _stateHandler?.Invoke($"Счёт пополнен на {sum}. {CurrentSumAsString}");
        }

        public void Withdraw(int sum)
        {
            if (sum <= CurrentSum)
            {
                CurrentSum -= sum;

                _stateHandler?.Invoke($"Сумма {sum} снята со счёта. {CurrentSumAsString}");
            }
            else
            {
                _stateHandler?.Invoke($"Недостаточно денег на счёте. {CurrentSumAsString}") ;
            }
        }

        private string CurrentSumAsString => $"Баланс: {CurrentSum}";
    }
}
