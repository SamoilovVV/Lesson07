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

        public int CurrentSum { get; private set; }

        public void RegisterStateHandler(AccountStateHandler stateHandler)
        {
            _stateHandler += stateHandler;
        }

        public void UnregisterStateHandler(AccountStateHandler stateHandler)
        {
            _stateHandler -= stateHandler;
        }

        public void Put(int sum)
        {
            if (sum > 0)
            {
                CurrentSum += sum;
                _stateHandler?.Invoke($"Счёт пополнен на {sum}. {CurrentSumAsString}");
            }
            else
            {
                _stateHandler?.Invoke($"Сумма пополнения должна быть положительной! {CurrentSumAsString}");
            }
            
        }

        public void Take(int sum)
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
