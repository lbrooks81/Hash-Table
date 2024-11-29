using BenchmarkDotNet.Running;

namespace HashTable
{
    public class Program
    {
        public static void Main()
        {
#if DEBUG
            TestMyHashTable();
#endif
            BenchmarkRunner.Run<BenchmarkWeek7>();
        }

        private static void TestMyHashTable()
        {
            MyHashTable<String, float> menu = new MyHashTable<String, float>();
            menu.Add("Pizza", 9.99f);
            menu.Add("Burger", 5.99f);
            menu.Add("Fries", 2.99f);
            menu.Add("Soda", 1.99f);
            menu.Add("Salad", 4.99f);
            menu.Add("Steak", 12.99f);

            Console.WriteLine(menu);
            Console.WriteLine("Count: " + menu.Count);
            Console.WriteLine("Buckets: " + menu.Buckets);
            Console.WriteLine("Price of Burger: " + menu["Burger"]);
        }
    }
}
