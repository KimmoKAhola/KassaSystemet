using static System.Net.Mime.MediaTypeNames;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Discount discount = new Discount(new DateTime(2023, 9, 20), new DateTime(2023, 9, 21), 5.00m);
            Discount.discountDictionary.Add("Bananer", discount);
            Discount.PrintDiscount(Discount.discountDictionary);
            //Menu.MainMenu();
        }
    }
}