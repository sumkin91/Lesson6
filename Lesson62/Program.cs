using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата. 
а) Сделать меню с различными функциями и представить пользователю выбор, для какой функции и на каком отрезке находить минимум. 
Использовать массив (или список) делегатов, в котором хранятся различные функции.
б) *Переделать функцию Load, чтобы она возвращала массив считанных значений. Пусть она возвращает минимум через параметр 
(с использованием модификатора out). 
*/
namespace Lesson62
{
    class Program
    {
        public delegate double Func(double x);
        
        public static double F(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static double C(double x)
        {
            return 1 / Math.Sin(Math.Pow(x, 2));
        }

        public static double K(double x)
        {
            return Math.Sqrt(Math.Pow((x / 3),2)) / Math.Sqrt(Math.Pow((x / 23),6));
        }

        public static void SaveFunc(string fileName, double a, double b, double h)
        {
            Func EnterFunc = null;
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            Console.WriteLine("Выберите одну из функций (1, 2 или 3):");
            int num = 0;
            while (true)
            {
                char key = Console.ReadKey().KeyChar;
                if (Int32.TryParse(key.ToString(), out num))
                {
                    if (num == 1 || num == 2 || num == 3) break;
                    else Console.WriteLine("\nНажата не та кнопка цифры!");
                }
                else Console.WriteLine("\nНажата кнопка буквенного символа!");
            }
            switch (num)
            {
                case 1: EnterFunc = F; break;
                case 2: EnterFunc = C; break;
                case 3: EnterFunc = K; break;
            }
            double x = a;
            while (x <= b)
            {
                
                bw.Write(EnterFunc(x));
                x += h;// x=x+h;
            }
            bw.Close();
            fs.Close();
        }
        public static List<double> Load(string fileName, out double minOut)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            List<double> list = new List<double>();
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                list.Add(d);
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            minOut = min;
            return list;
        }
        static void Main(string[] args)
        {
            SaveFunc("data.bin", -100, 100, 0.5);
            List<double> listOut = Load("data.bin", out double min);
            Console.WriteLine("\nМинимальное значение равно: {0}", min);
            Console.WriteLine("Вывод значений из файла 'data.bin':");
            foreach(double item in listOut)
            {
                Console.Write("{0:0.00}  ", item);
            }
            Console.ReadKey();
        }
    }

}
