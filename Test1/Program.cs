using System;

namespace Test1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Console.WriteLine("---");
            Task2();
        }

        static private void Task1()
        {

            List<string> nameList = new List<string>
            {
                "Tom",
                "Alice",
                "Bob",
                "Sam",
                "Tim",
                "Tomas",
                "Bill"
            };

            IEnumerable<string> query = from name in nameList
                                        where name.Length == 3
                                        select name;

            foreach (string str in query)
            {
                Console.WriteLine(str);
            }
        }

        static private void Task2()
        {
            List<int> intList = new List<int> { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };

            IEnumerable<int> query = from num in intList
                                     where num > 10
                                     where (num % 2) == 0
                                     select num;

            foreach (int num in query)
            {
                Console.WriteLine(num);
            }
        }
    }
}