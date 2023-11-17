
using KassaSystemet.Interfaces;
using System.Diagnostics;
using System.IO;
using KassaSystemet.File_IO;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Data_Seeding;

namespace KassaSystemet.Models
{
    public class ProductCatalogue
    {
        private ProductCatalogue()
        {
            Products = new();
            Discounts = new();
        }
        private static ProductCatalogue instance;
        public Dictionary<int, Product> Products { get; set; }
        public List<Discount> Discounts { get; set; }
        public static ProductCatalogue Instance => instance ??= new ProductCatalogue();
        private static string[] GetWares()
        {
            string[] wares;
            if (File.Exists(FileManagerOperations.CreateSeededProductsFilePath()))
            {
                wares = File.ReadAllText(FileManagerOperations.CreateSeededProductsFilePath()).Split('\n');
            }
            else
                wares = SeededProducts._seededProducts;
            return wares;
        }

        public static Dictionary<int, Product> SeedProducts()
        {
            Dictionary<int, Product> productDatabase = new();
            string[] products = GetWares();
            foreach (var item in products)
            {
                var temp = item.Split('!');

                int id = Convert.ToInt32(temp[0]);
                string name = temp[1].Trim();
                decimal price = Convert.ToDecimal(temp[2]);
                string type = temp[3];
                var product = ModelFactory.CreateProduct(name, price, type);
                productDatabase.Add(id, product);

            }
            return productDatabase;
        }
        public void AddNewProduct(int productId, IUserInputHandler userInputHandler)
        {
            var info = userInputHandler.NewProduct();
            var productName = info.Item1;
            var price = info.Item2;
            var priceType = info.Item3;
            var product = ModelFactory.CreateProduct(productName, price, $"{priceType}");
            Products.Add(productId, product);
            PrintSuccessMessage($"Added the product {product.ProductName} with ID [{productId}] to the system.");
        }
        public void AddNewDiscount(int productId, IUserInputHandler userInputHandler, out bool result)
        {
            var info = userInputHandler.DiscountInput();
            var startDate = info.Item1;
            var endDate = info.Item2;
            var discountPercentage = info.Item3;

            if (startDate.CompareTo(endDate) < 0)
            {
                var discount = ModelFactory.CreateDiscount(startDate, endDate, discountPercentage);
                Products[productId].AddDiscountToProduct(discount);
                PrintSuccessMessage($"Your discount {startDate}-{endDate} {discountPercentage} % has been added.");
                result = true;
            }
            else
            {
                PrintErrorMessage("The discount's start date can not be after later than the end date. Your discount has not been added.");
                result = false;
            }
        }
        public void RemoveProduct(int productId) => Products.Remove(productId);
        public bool ContainsDiscount(int productId) => Products.ContainsKey(productId) && Products[productId].Discounts.Count > 0;
        public void DisplayProducts()
        {
            foreach (var item in Products.OrderBy(x => x.Key))
            {
                if (item.Value.Discounts.Count > 0 && item.Value.HasActiveDiscount())
                {
                    decimal bestDiscount = item.Value.GetBestDiscount();
                    PrintMessage($"Product ID: {item.Key}, {item.Value} - {bestDiscount:C2} % discount!");
                }
                else
                    PrintMessage($"Product ID: {item.Key}, {item.Value}");
            }
        }
        public IEnumerable<Product> GetAllDiscounts() => Products.Values.Where(p => p.Discounts.Count > 0);
        public static void DisplayAllDiscounts()
        {
            var discountedProducts = Instance.GetAllDiscounts();
            if (discountedProducts.ToList().Count > 0)
            {
                discountedProducts.ToList().ForEach(product =>
                {
                    PrintMessage($"The product: {product.ProductName} has the following discounts: ");
                    product.Discounts.ForEach(discount => PrintMessage(discount.ToString()));
                });
            }
            else
                PrintErrorMessage("No discounts available in the system.");
        }
        private static void PrintSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintMessage(string message) => Console.WriteLine(message);
    }
}