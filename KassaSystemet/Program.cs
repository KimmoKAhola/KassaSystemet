using static System.Net.Mime.MediaTypeNames;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string input = "Bananer";
            string input2 = "Bananer";
            string startDate = "2023/09/20";
            string startDate2 = "2023/09/26";
            string endDate = "2023/09/25";
            string endDate2 = "2023/09/28";
            decimal discountPercentage = 80m;
            decimal discountPercentage2 = 50m;
            //Discount.AddNewDiscount(input, startDate, endDate, discountPercentage);
            //Discount.AddNewDiscount(input2, startDate2, endDate2, discountPercentage2);

            Dictionary<int, Product> products = FileManager.LoadProductList();
            //Product.DisplayProducts(products);
            Dictionary<string, List<Discount>> test = FileManager.LoadDiscountList();
            Discount.DisplayAllDiscounts(test);
            Menu.MainMenu();
        }
    }
}