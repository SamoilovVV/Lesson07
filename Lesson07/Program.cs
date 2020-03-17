using System;
using System.Collections.Generic;

namespace UsingDelegates
{
    public delegate void Notifier(); // Объявление делегата
    class Program
    {

        static void Main(string[] args)
        {
            //VerySimpleDelegateUsage();
            // UsingDelegatesWithParameters();
            // HowToCreateDelegates();
            // UniteDelegates();
            //AddMethodsToDelegate();
            //TestGenericDelegates();
            // DelegatesAsMethodParams();
            // TestCovariance();
            //TestContravariance();

            //PracticalDelegatesUsage();
            //DelegatesWithAnonimousMethods();
            //Actions();
            //Funcs();
            //Predicates();
            Lambdas();

            Console.ReadKey();
        }

        public static void VerySimpleDelegateUsage()
        {
            Notifier hello = SayHello; // Инициализация переменной-делегата

            hello(); // Вызов метода через делегат
        }
        private static void SayHello()
        {
            Console.WriteLine("Hello World!");
        }

        private delegate int PerformCalculation(int x, int y); // Объявление делегата с параметрами
        public static void UsingDelegatesWithParameters()
        {
            PerformCalculation operation = Add;
            int result = operation(x:2, y:3);
            Console.WriteLine($"Результат операции: {result}");

            operation = Multiply;
            result = operation(2, 3);
            Console.WriteLine($"Результат операции: {result}");
        }

        static int Add(int x, int y)
        {
            return x + y;
        }

        static int Multiply(int x, int y)
        {
            return x * y;
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

        static void SayGoodBye()
        {
            Console.WriteLine("Good Bye!");
        }
        public static void AddMethodsToDelegate()
        {
            Notifier helloAndGoodBye = SayHello;
            helloAndGoodBye += SayHowAreYou; // Добавление метода в делегат
            helloAndGoodBye += SayHowAreYou;
            helloAndGoodBye += SayGoodBye;
            helloAndGoodBye += SayGoodBye;

            helloAndGoodBye(); // Вызываются все добавленные методы
            Console.WriteLine("\n");
            helloAndGoodBye -= SayHowAreYou; // Удаление метода из делегата
            helloAndGoodBye -= SayGoodBye;
            helloAndGoodBye -= SayHowAreYou;
            helloAndGoodBye -= SayHowAreYou;
            helloAndGoodBye();

            Console.WriteLine("\n");
            PerformCalculation operation = Add;
            operation += Multiply;

            int result = operation(2, 3);
            Console.WriteLine($"Результат {result}");

        }

        static void SayHowAreYou()
        {
            Console.WriteLine("How are you?");
        }

        delegate T Operation<T, V>(V val); // Обобщённый делегат
        public static void TestGenericDelegates()
        {
            Operation<double, int> operation = SquareRoot;

            var result = operation(5);

            Console.WriteLine($"Результат операции {result}");

            Operation<string, int> operation2 = SquareRootAsString;
            Console.WriteLine(operation2(5));
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
        public static void DelegatesAsMethodParams()
        {
            ShowNotification(null);
            Notifier notifier = SayHello;
            ShowNotification(notifier); // Передача делегата в явном виде

            ShowNotification(SayGoodBye); // Передача делегата в неявном виде
        }

        static void ShowNotification(Notifier notifier)
        {
            notifier?.Invoke();
        }

        delegate Person PersonFactory(string name);
        public static void TestCovariance()
        {
            PersonFactory personFactory = BuildCustomer;

            var person = personFactory("Вася");

            Console.WriteLine(person.Name);
        }


        static Customer BuildCustomer(string name)
        {
            return new Customer { Name = name, Id = Guid.NewGuid().ToString() };
        }

        delegate void CustomerInfo(Customer customer);
        public static void TestContravariance()
        {
            CustomerInfo customerInfo = GetPersonInfo;

            var customer = new Customer { Name = "Петя", Id = Guid.NewGuid().ToString() };
            
            customerInfo.Invoke(customer);
        }

        static void GetPersonInfo(Person person)
        {
            Console.WriteLine(person.Name);
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
            account.Take(300);
            account.Take(150);

            // Удаляем делегат
            account.UnregisterStateHandler(accountStateHandler);
            account.Take(100);

            account.Put(50);
            account.Put(-50);
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

        delegate void MessageHandler(string message);
        public static void DelegatesWithAnonimousMethods()
        {
            int x = 5;
            double d = 6.0;
            MessageHandler messageHandler = delegate (string message)
            {
                if (x < 0.0 && d < 0.0)
                    return;

                Console.WriteLine(message);
            };

            messageHandler.Invoke("Hello World");

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
            // delegate void MessageHandler(string message);

            //MessageHandler message = delegate (string message)
            // То же самое:
            Action<string> message = delegate (string message)
            {
                Console.WriteLine(message);
            };

            message("Hello Actions");

            Action<int, int> operation = Divide;
            PerformOperation(5, 2, operation);

        }

        static void Divide(int x, int y)
        {
            if (y != 0)
            {
                Console.WriteLine($"Результат целочисленного деления {x / y}");
            }
            else
            {
                Console.WriteLine("На ноль делить нельзя!");
            }
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

            Predicate<Point> findPointPredicate = delegate (Point pt)
            {
                return (pt.X + pt.Y > 500);
            };
            Point pt = points.Find(findPointPredicate);

            Console.WriteLine($"Найдена точка: X = {pt.X}, Y = {pt.Y}");
        }

        static bool FindPoint(Point pt)
        {
            return (pt.X + pt.Y > 500);
        }

        public static void Lambdas()
        {
            /*PerformCalculation operation = delegate (int x, int y)
            {
                return x - y;
            };*/

            PerformCalculation operation = (x, y) => x - y;

            Console.WriteLine(operation(2, 3));

            /*Action<string> message = delegate (string message)
            {
                Console.WriteLine(message);
            };*/
            // То же самое:
            Action<string> message = msg => Console.WriteLine(msg);
            
            message("Hello");

            var points = new List<Point> { new Point(100, 200), new Point(150, 250), new Point(350, 400), new Point(125, 225) };
            
            // var pt = points.Find(FindPoint);
            // То же самое:
            var pt = points.Find(p => p.X + p.Y > 500);

            Console.WriteLine($"Найдена точка: X = {pt.X}, Y = {pt.Y}");
        }



        

        static void PerformOperation(int x, int y, Action<int, int> operation)
        {
            operation?.Invoke(x, y);
        }

        

        static int PerformMultiplication(int x, int y, Func<int, int, int> operation)
        {
            return operation.Invoke(x, y);
        }


}
}
