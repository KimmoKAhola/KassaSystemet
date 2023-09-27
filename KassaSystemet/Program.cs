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
        }
    }
}