
using KassaSystemet.Interfaces;
using System.Diagnostics;
using System.IO;
using KassaSystemet.Strategy;
using KassaSystemet.Factories.ModelFactory;

namespace KassaSystemet.Models
{
    public class ProductCatalogue
    {
        ILoad _fileManager = new DefaultFileManager();
        private ProductCatalogue()
        {
            Products = _fileManager.LoadProductListFromFile();
        }
        private static ProductCatalogue instance;
        public Dictionary<int, Product> Products { get; }
        public List<Discount> Discounts { get; }
        public static ProductCatalogue Instance => instance ??= new ProductCatalogue();
        private static string[] _wares = File.ReadAllText(FileManagerOperations.CreateSeededProductsFilePath()).Split('\n');

        public static Dictionary<int, Product> SeedProducts()
        {
            Dictionary<int, Product> productDatabase = new();
            string[] products = _wares;
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
            Console.WriteLine($"Added the product {product.ProductName} with ID [{productId}] to the system.", Console.ForegroundColor = ConsoleColor.Green);
        }
        public void AddNewDiscount(int productId, IUserInputHandler userInputHandler)
        {
            var info = userInputHandler.DiscountInput();
            var startDate = info.Item1;
            var endDate = info.Item2;
            var discountPercentage = info.Item3;

            if (startDate.CompareTo(endDate) < 0)
            {
                var discount = ModelFactory.CreateDiscount(startDate, endDate, discountPercentage);
                Products[productId].AddDiscountToProduct(discount);
                Console.WriteLine($"Your discount {startDate}-{endDate} {discountPercentage} % has been added.", Console.ForegroundColor = ConsoleColor.Green);
            }
            else
                Console.WriteLine("The discount's start date can not be after later than the end date. Your discount has not been added.", Console.ForegroundColor = ConsoleColor.Red);
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
                    Console.WriteLine($"Product ID: {item.Key}, {item.Value} - {bestDiscount * 100m} % discount!");
                }
                else
                    Console.WriteLine($"Product ID: {item.Key}, {item.Value}");
            }
        }
        public IEnumerable<Product> GetAllDiscounts() => Products.Values.Where(p => p.Discounts.Count > 0);
        public static void DisplayAllDiscounts()
        {
            var discountedProducts = Instance.GetAllDiscounts();

            discountedProducts.ToList().ForEach(product =>
            {
                Console.WriteLine($"The product: {product.ProductName} has the following discounts: ");
                product.Discounts.ForEach(discount => Console.WriteLine(discount.ToString()));
            });
        }
    }
}