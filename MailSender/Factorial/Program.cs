using System;
using System.Text;
using System.Threading;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //Factorial(10);
            //Sum(10);
            //new Thread(() => Factorial(10)).Start();
            //new Thread(() => Sum(10)).Start();
            var z = new FactorialCalculation(10);
            z.ResultCompleted += getResult;
            z.Start();
            Console.WriteLine("Ждем завершение вычисления факториала");

            Console.ReadKey();
        }

        private static void getResult(int e)
        {
            Console.WriteLine("Factorial result {0}", e);
        }

        static void Factorial(int n)
        {
            StringBuilder _string = new StringBuilder();
            if (n <= 1 || n < 0) {
                Console.WriteLine($"Factorial {n} = 1");
                return;
            }
            int factorial=1;
            _string.Append($"Factorial {n} = 1");
            for (int i = 2; i<=n; i++)
            {
                _string.Append($" * {i}");
                factorial *= i;
            }
            _string.Append($" = {factorial}");
            Console.WriteLine(_string);

        }
        static void Sum(int n)
        {
            StringBuilder _string = new StringBuilder();
            int sum = 0;
            _string.Append($"Summ {n} = ");
            for (int i = 1; i <= n; i++)
            {
                if (i == 1) _string.Append($"{i}");
                else _string.Append($" + {i}");
                sum += i;
            }
            _string.Append($" = {sum}");
            Console.WriteLine(_string);
            //return sum;
        }
    }
    class FactorialCalculation
    {
        private readonly int __n;
        public delegate void Result(int e);
        public event Result ResultCompleted;
        private Thread _Thread;
        public FactorialCalculation(int n)
        {
            __n = n;
            _Thread = new Thread(ThreadMethod) { IsBackground = true };
        }
        public void Start()
        {
            if (!_Thread.IsAlive) _Thread.Start();
        }
        private void ThreadMethod()
        {
            if (__n <= 1 || __n < 0)
            {
                ResultCompleted?.Invoke(1);
                return;
            }
            int factorial = 1;
            for (int i = 2; i <= __n; i++)
            {
                factorial *= i;
            }
            ResultCompleted.Invoke(factorial);
        }

    }
}
