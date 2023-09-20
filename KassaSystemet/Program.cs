using static System.Net.Mime.MediaTypeNames;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Discount discount = new Discount("2023/09/20", "2023/09/25", 5.00m);
            //Discount.discountDictionary.Add("Bananer", discount);
            //Discount.PrintDiscount(Discount.discountDictionary);
            //discount.IsProductOnSale();
            Receipt.Test(Seed.seedProductList, Seed.discountDictionary);
            //Menu.MainMenu();
        }
    }
}