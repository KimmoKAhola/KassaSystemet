using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManager.LoadProductList();
            FileManager.LoadDiscountList();
            Menu menu = new();
            menu.MainMenu();

            //DateOnly date = new DateOnly();
            //DateOnly date = new DateOnly(2023, 09, 25);

            //Console.WriteLine(date);

            //Console.Write("Enter a date YYYY-MM-DD");
            //string input = Console.ReadLine();

            //string[] dateInfo = input.Split('-');
            //foreach (var item in dateInfo)
            //{
            //    Console.WriteLine(item);
            //}

            //Dictionary<int, string> temp = new Dictionary<int, string>();
            //temp.Add(300, "A");
            //temp.Add(302, "B");
            //temp.Add(305, "C");
            //temp.Add(301, "D");
            //temp.Add(299, "E");
            //temp.Add(280, "F");

            //foreach (var item in temp)
            //{
            //    Console.WriteLine(item.Key + ", " + item.Value);
            //}
            //temp = temp.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            //foreach (var item in temp)
            //{
            //    Console.WriteLine(item.Key + ", " + item.Value);
            //}
        }
    }
}