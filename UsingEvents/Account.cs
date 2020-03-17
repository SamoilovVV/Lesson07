using System;

namespace UsingEvents
{
    public delegate void AccountStateHandler(string message);

    public delegate void AccountHandlerWithArgs(object sender, AccountEventArgs e);

    interface IAccount
    {
        event AccountStateHandler AccountHandler;
    } 
    class Account : IAccount
    {
        private event AccountStateHandler _accountHandler;
        public event AccountStateHandler AccountHandler
        {
            add
            {
                _accountHandler += value;
                Console.WriteLine($"{value.Method.Name} добавлен");
            }
            remove
            {
                _accountHandler -= value;
                Console.WriteLine($"{value.Method.Name} удалён");
            }
        }

        public event AccountHandlerWithArgs Notify;

        public event EventHandler StandardNotify;
        public event EventHandler<AccountEventArgs> StandardNotifyWithArgs;

        public Account(int sum)
        {
            CurrentSum = sum;
        }

        public int CurrentSum { get; private set; }

        public void Put(int sum)
        {
            if (sum > 0)
            {
                CurrentSum += sum;
                _accountHandler?.Invoke($"Счёт пополнен на {sum}. {CurrentSumAsString}");
                Notify?.Invoke(this, new AccountEventArgs($"Счёт пополнен на {sum}. {CurrentSumAsString}", sum));

                StandardNotify?.Invoke(this, EventArgs.Empty);
                StandardNotifyWithArgs?.Invoke(this, new AccountEventArgs($"Счёт пополнен на {sum}. {CurrentSumAsString}", sum));
            }
            else
            {
                _accountHandler?.Invoke($"Сумма пополнения должна быть положительной! {CurrentSumAsString}");
                Notify?.Invoke(this, new AccountEventArgs($"Сумма пополнения должна быть положительной! {CurrentSumAsString}", sum));
                
                StandardNotifyWithArgs.Invoke(this, new AccountEventArgs($"Сумма пополнения должна быть положительной! {CurrentSumAsString}", sum));
            }
            
        }

        public void Take(int sum)
        {
            if (sum <= CurrentSum)
            {
                CurrentSum -= sum;

                _accountHandler?.Invoke($"Сумма {sum} снята со счёта. {CurrentSumAsString}");
                Notify?.Invoke(this, new AccountEventArgs($"Сумма {sum} снята со счёта. {CurrentSumAsString}", sum));

                StandardNotify.Invoke(this, EventArgs.Empty);
                StandardNotifyWithArgs?.Invoke(this, new AccountEventArgs($"Сумма {sum} снята со счёта. {CurrentSumAsString}", sum));
            }
            else
            {
                _accountHandler?.Invoke($"Недостаточно денег на счёте. {CurrentSumAsString}") ;
                Notify?.Invoke(this, new AccountEventArgs($"Недостаточно денег на счёте. {CurrentSumAsString}", sum));
                
                StandardNotifyWithArgs?.Invoke(this, new AccountEventArgs($"Недостаточно денег на счёте. {CurrentSumAsString}", sum));
            }
        }

        private string CurrentSumAsString => $"Баланс: {CurrentSum}";
    }

    public class AccountEventArgs : EventArgs
    {
        public string Message { get; }

        public int Sum { get; }

        public AccountEventArgs(string message, int sum)
        {
            Message = message;
            Sum = sum;
        }
    }
}
