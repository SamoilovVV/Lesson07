using System;
using System.Collections.Generic;

namespace UsingDelegates
{
    class Program
    {
        delegate void Notifier(); // Объявление делегата
        
        delegate int PerformCalculation(int x, int y); // Объявление делегата с параметрами

        delegate T Operation<T, V>(V val); // Обобщённый делегат

        delegate Person PersonFactory(string name);
        delegate void CustomerInfo(Customer customer);

        delegate void MessageHandler(string message);

        static void Main(string[] args)
        {
            VerySimpleDelegateUsage();
            UsingDelegatesWithParameters();
            HowToCreateDelegates();
            UniteDelegates();
            AddMethodsToDelegate();
            TestGenericDelegates();
            DelegatesAsMethodParams();
            TestCovariance();
            TestContravariance();

            PracticalDelegatesUsage();
            DelegatesWithAnonimousMethods();
            Actions();
            Funcs();
            Predicates();
            Lambdas();

            Console.ReadKey();
        }

        public static void VerySimpleDelegateUsage()
        {
            Notifier hello = SayHello; // Инициализация переменной-делегата

            hello(); // Вызов метода через делегат
        }

        public static void UsingDelegatesWithParameters()
        {
            PerformCalculation operation = Add;
            int result = operation(2, 3);
            Console.WriteLine($"Результат операции: {result}");

            operation = Multiply;
            result = operation(2, 3);
            Console.WriteLine($"Результат операции: {result}");
        }

        public static void HowToCreateDelegates()
        {
            PerformCalculation operation = Add; // Создание объекта делегата прямым присвоением метода
            int result = operation(2, 3);
            Console.WriteLine($"Результат операции: {result}");

            var operation2 = new PerformCalculation(Add); // Создание объекта делегата с помощью конструктора
            int result2 = operation2(2, 3);
            Console.WriteLine($"Результат операции: {result2}");
        }

        public static void UniteDelegates()
        {
            Notifier hello = SayHello;

            Notifier goodBye = SayGoodBye;

            Notifier helloAndGoodBye = hello + goodBye;

            helloAndGoodBye();

            var goodByeOnly = helloAndGoodBye - goodBye;
            goodByeOnly();
        }

        public static void AddMethodsToDelegate()
        {
            Notifier helloAndGoodBye = SayHello;
            helloAndGoodBye += SayHowAreYou; // Добавление метода в делегат
            helloAndGoodBye += SayGoodBye;

            helloAndGoodBye(); // Вызываются все добавленные методы

            helloAndGoodBye -= SayHowAreYou; // Удаление метода из делегата

            helloAndGoodBye();
        }

        public static void TestGenericDelegates()
        {
            Operation<double, int> operation = SquareRoot;

            var result = operation(5);

            Console.WriteLine($"Результат операции {result}");

            Operation<string, int> operation2 = SquareRootAsString;
            Console.WriteLine(operation2(5));
        }

        public static void DelegatesAsMethodParams()
        {
            Notifier notifier = SayHello;
            ShowNotification(notifier); // Передача делегата в явном виде

            ShowNotification(SayGoodBye); // Передача делегата в неявном виде
        }

        public static void TestCovariance()
        {
            PersonFactory personFactory = BuildCustomer;

            var person = personFactory("Вася");

            Console.WriteLine(person.Name);
        }

        public static void TestContravariance()
        {
            CustomerInfo customerInfo = GetPersonInfo;

            var customer = new Customer { Name = "Петя", Id = Guid.NewGuid().ToString() };
            
            customerInfo.Invoke(customer);
        }

        public static void PracticalDelegatesUsage()
        {
            Account account = new Account(500);

            // Создаём делегат
            Account.AccountStateHandler accountStateHandler = AccountStateColorMessage;

            // Регестрируем два делегата
            account.RegisterStateHandler(accountStateHandler);
            account.RegisterStateHandler(AccountStateMessage);

            // Снимаем деньги со счёта
            account.Withdraw(300);
            account.Withdraw(150);

            // Удаляем делегат
            account.UnregisterStateHandler(accountStateHandler);
            account.Withdraw(100);
        }

        public static void DelegatesWithAnonimousMethods()
        {
            MessageHandler messageHandler = delegate (string message)
            {
                Console.WriteLine(message);
            };

            messageHandler("Hello World");

            Notifier notifier = delegate
            {
                Console.WriteLine("How are you?");
            };

            notifier();

            PerformCalculation operation = delegate (int x, int y)
            {
                return x - y;
            };

            int result = operation(2, 3);
            Console.WriteLine(result);
        }

        public static void Actions()
        {
            Action<string> message = delegate (string message)
            {
                Console.WriteLine(message);
            };

            message("Hello Actions");

            Action<int, int> operation = Divide;
            PerformOperation(5, 2, operation);

        }

        public static void Funcs()
        {
            Func<int, int, int> sum = delegate (int x, int y)
            {
                return x + y;
            };

            Console.WriteLine(sum);

            Func<int, int, int> mult = Multiply;
            Console.WriteLine(mult(2,3));

            int result = PerformMultiplication(2, 3, Multiply);

            Console.WriteLine(result);
        }

        public static void Predicates()
        {
            var points = new List<Point> { new Point(100, 200), new Point(150, 250), new Point(350, 400), new Point(125, 225) };

            Predicate<Point> findPointPredicate = FindPoint;
            Point pt = points.Find(findPointPredicate);

            Console.WriteLine($"Найдена точка: X = {pt.X}, Y = {pt.Y}");
        }

        public static void Lambdas()
        {
            PerformCalculation operation = (x, y) => x - y;

            Console.WriteLine(operation(2, 3));

            Action<string> message = msg => Console.WriteLine(msg);
            
            message("Hello");

            var points = new List<Point> { new Point(100, 200), new Point(150, 250), new Point(350, 400), new Point(125, 225) };

            var pt = points.Find(p => p.X + p.Y > 500);

            Console.WriteLine($"Найдена точка: X = {pt.X}, Y = {pt.Y}");
        }

        static void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
        static void SayHowAreYou()
        {
            Console.WriteLine("How are you?");
        }

        static void SayGoodBye()
        {
            Console.WriteLine("Good Bye!");
        }

        static int Add(int x, int y)
        {
            return x + y;
        }

        static int Multiply(int x, int y)
        {
            return x * y;
        }

        static double SquareRoot(int n)
        {
            return Math.Sqrt(n);
        }

        static string SquareRootAsString(int n)
        {
            var result = SquareRoot(n);
            return result.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        static void ShowNotification(Notifier notifier)
        {
            notifier?.Invoke();
        }

        static Customer BuildCustomer(string name)
        {
            return new Customer { Name = name, Id = Guid.NewGuid().ToString() };
        }

        static void GetPersonInfo(Person person)
        {
            Console.WriteLine(person.Name);
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

        static void PerformOperation(int x, int y, Action<int, int> operation)
        {
            operation?.Invoke(x, y);
        }

        static void Divide(int x, int y)
        {
            if (y != 0)
            {
                Console.WriteLine($"Результат целочисленного деления {x/y}");
            }
            else
            {
                Console.WriteLine("На ноль делить нельзя!");
            }
        }

        static int PerformMultiplication(int x, int y, Func<int, int, int> operation)
        {
            return operation.Invoke(x, y);
        }

        static bool FindPoint(Point pt)
        {
            return (pt.X + pt.Y > 500);
        }
}
}
