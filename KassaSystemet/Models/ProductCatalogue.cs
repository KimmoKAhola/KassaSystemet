
using KassaSystemet.Interfaces;
using System.Diagnostics;
using System.IO;
using KassaSystemet.Strategy;
using KassaSystemet.Factories.ModelFactory;

namespace KassaSystemet.Models
{
    public class ProductCatalogue
    {
        FileManager _fileManager = new FileManager(new FileManagerStrategy());
        private ProductCatalogue()
        {
            Products = _fileManager.LoadProductList();
        }
        private static ProductCatalogue instance;
        public Dictionary<int, Product> Products { get; }
        public static ProductCatalogue Instance => instance ??= new ProductCatalogue();
        private static string _wares =
            "300!Bananer!15,50!per kg!" +
            "301!Äpplen!25,50!per kg!" +
            "302!Kaffe!65,50!per unit!" +
            "303!Choklad!19,90!per unit!" +
            "304!Lösgodis!89,90!per kg!" +
            "305!Rågbröd!55,00!per unit!" +
            "306!Toalettpapper!32,00!per unit!" +
            "307!Kex!25,60!per unit!" +
            "308!Vattenmelon!55,00!per kg!" +
            "309!Smör!79,00!per kg!" +
            "310!Gott & Blandat!29,00!per unit!" +
            "311!Hushållsost!79,00!per kg!" +
            "312!Kycklingfilé!119,00!per kg!" +
            "313!Yoggi!40,00!per unit!" +
            "314!Tomater på burk!11,00!per unit!" +
            "315!Stekpanna!339,00!per unit!" +
            "316!Dammsugare!999,99!per unit!" +
            "317!Västerbottensost!10,00!per kg!" +
            "318!Oxfilé!399,99!per kg!" +
            "319!Päron!35,99!per kg!" +
            "320!Pasta!19,99!per unit!";
        public Dictionary<int, Product> SeedProducts()
        {
            Dictionary<int, Product> productDatabase = new();
            string[] products = _wares.Split('!');

            for (int i = 0; i < products.Length - 1; i += 4)
            {
                int id = Convert.ToInt32(products[i]);
                string name = products[i + 1].Trim();
                decimal price = Convert.ToDecimal(products[i + 2]);
                string type = products[i + 3];
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