using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Изменить программу вывода таблицы функции так, чтобы можно было передавать функции типа double (double, double). 
 Продемонстрировать работу на функции с функцией a* x^2 и функцией a* sin(x).
*/
namespace Lesson61
{
    class Program
    {

        public delegate void EnterFuncDelegate(double a, double x);

        public static void Func1(double a, double x)
        {
            Console.WriteLine("Вывод значения функции a*x^2 при a = {0}, x = {1}: {2:0.00}" , a, x, Math.Pow(x,2)*a);
        }

        public static void Func2(double a, double x)
        {
            Console.WriteLine("Вывод значения функции a*sin(x) при a = {0}, x = {1}: {2:0.00}" , a, x, Math.Sin(x) * a);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Вывод функций типа a*x^2 и a*sin(x)");
            double a = 0;
            double x = 0;
            while(true)
            {
                Console.Write("Введите a = ");
                if (Double.TryParse(Console.ReadLine(), out a)) break;
                else Console.WriteLine("Повторите ввод!");
            }
            while (true)
            {
                Console.Write("Введите x = ");
                if (Double.TryParse(Console.ReadLine(), out x)) break;
                else Console.WriteLine("Повторите ввод!");
            }
            EnterFuncDelegate EnterFunc = Func1;
            EnterFunc += Func2;
            EnterFunc(a, x);
            Console.ReadKey();
        }
    }
}
