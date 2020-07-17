using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
/*Переделать программу Пример использования коллекций для решения следующих задач:
а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(*частотный массив);
в) отсортировать список по возрасту студента;
г) * отсортировать список по курсу и возрасту студента;
*/
namespace Lesson63
{
    class Student
    {
        public string lastName;
        public string firstName;
        public string university;
        public string faculty;
        public int course;
        public string department;
        public int group;
        public string city;
        public int age;
        // Создаем конструктор
        public Student(string lastName, string firstName,  string university, string faculty, int course, string department, int group, string city, int age)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.university = university;
            this.faculty = faculty;
            this.department = department;
            this.course = course;
            this.age = age;
            this.group = group;
            this.city = city;
        }
    }

    class Program
    {
        public delegate int CompareStudent(Student st1, Student st2);
        
        static int CompareFirstName(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {
            return String.Compare(st1.firstName, st2.firstName);          // Сравниваем две строки
        }

        static int CompareAge(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {
            return (st1.age > st2.age) ? 1 : (st1.age < st2.age) ? -1 : 0;
        }

        static int CompareCourse(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {
            return (st1.course > st2.course) ? 1 : (st1.course < st2.course) ? -1 : 0;
        }

        static void Main(string[] args)
        {
            int bakalavr = 0;
            int magistr = 0;
            int count56 = 0;
            List<int> listCourseCount = new List<int>(6);
            List<Student> list = new List<Student>();                             // Создаем список студентов
            Dictionary<int, int> dict = new Dictionary<int, int>();
            CompareStudent compStudent = null;
            int dt = DateTime.Now.Millisecond;
            StreamReader sr = new StreamReader("students.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    // Добавляем в список новый экземпляр класса Student
                    list.Add(new Student(s[0], s[1], s[2], s[3], int.Parse(s[4]), s[5], int.Parse(s[6]), s[7], int.Parse(s[8])));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                    // Выход из Main
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            //a
            foreach(Student item in list)
            {
                if (item.course == 5 || item.course == 6) count56++;
            }
            //b
            foreach(Student item in list)
            {
                if(item.age >= 18 && item.age <= 20){
                    if (dict.ContainsKey(item.course)) dict[item.course] += 1;
                    else dict.Add(item.course, 1);
                }
            }
            sr.Close();
            compStudent = CompareAge;
            list.Sort(new Comparison<Student>(compStudent));
            Console.WriteLine("Список по возрасту:");
            foreach (var v in list)
            {
                Console.WriteLine($"{v.lastName}\t{v.firstName}\t{v.university}\t{v.faculty}\t{v.course}\t{v.department}\t{v.group}\t{v.city}\t{v.age}");//в
            }
            //г
            compStudent = CompareCourse;
            compStudent += CompareAge;
            Console.WriteLine("Список по курсу и возрасту:");
            foreach (var v in list)
            {
                Console.WriteLine($"{v.lastName}\t{v.firstName}\t{v.university}\t{v.faculty}\t{v.course}\t{v.department}\t{v.group}\t{v.city}\t{v.age}");//г
            }
            Console.WriteLine("Студентов на 5 и 6 курсе всего:{0}", count56);//a
            foreach(KeyValuePair<int,int>pair in dict)
            {
                Console.WriteLine($"Студентов от 18 до 20 лет на {pair.Key} курсе: {pair.Value}");//б
            }
            Console.WriteLine($"Программа выполнена за {DateTime.Now.Millisecond - dt} мс");
            Console.ReadKey();
        }
    }
}
