using System;

namespace UsingEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestEvents();
            //TestEventsWithArgs();
            TestDefaultEvents();
        }

        public static void TestEvents()
        {
            Account account = new Account(500);

            // Добавляем обработчик события
            account.AccountHandler += AccountStateMessage;
            account.AccountHandler += AccountStateColorMessage;

            account.Put(200);

            // Удаляем обработчик события
            account.AccountHandler -= AccountStateColorMessage;

            // Снимаем деньги со счёта
            account.Take(300);
            account.Take(150);

            account.Take(300);
        }

        public static void TestEventsWithArgs()
        {
            Account account = new Account(100);

            account.Notify += OnAccountNotify;

            account.Put(20);
            account.Take(70);
            account.Take(150);
        }

        public static void TestDefaultEvents()
        {
            Account account = new Account(100);

            account.StandardNotify += OnAccountStandardNotify;

            account.StandardNotifyWithArgs += OnAccountStandardNotifyWithArgs;

            account.Put(20);
            account.Put(30);
        }

        private static void OnAccountStandardNotifyWithArgs(object sender, AccountEventArgs e)
        {
            if (e == null)
                return;

            Console.WriteLine($"Сумма: {e.Sum}\n Сообщение: {e.Message}");
        }

        private static void OnAccountStandardNotify(object sender, EventArgs e)
        {
            Console.WriteLine($"Баланс счёта изменился. Текущий баланс {(sender as Account).CurrentSum}");
        }

        private static void OnAccountNotify(object sender, AccountEventArgs e)
        {
            if (e == null)
                return;

            Console.WriteLine($"Сумма: {e.Sum}\n Сообщение: {e.Message}");
        }

        static void AccountStateMessage(string message)
        {
            Console.WriteLine(message);
        }

        static void AccountStateColorMessage(string message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }
    }
}
