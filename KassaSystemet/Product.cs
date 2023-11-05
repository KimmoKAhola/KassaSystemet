namespace KassaSystemet
{
    public class Product
    {
        public Product(string productName, decimal unitPrice, string priceType)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
            PriceType = priceType.ToLower();
            _discount = new();
        }
        private List<Discount> _discount;
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; }
        public List<Discount> Discounts => _discount;
        public static void DisplayAllDiscounts(Dictionary<int, Product> products)
        {
            var discountedProducts = GetAllDiscounts(products);

            discountedProducts.ToList().ForEach(product =>
            {
                Console.WriteLine($"The product: {product.ProductName} has the following discounts: ");
                product.Discounts.ForEach(discount => Console.WriteLine(discount.ToString()));
            });
        }
        public static IEnumerable<Product> GetAllDiscounts(Dictionary<int, Product> products) => products.Values.Where(p => p.Discounts.Count > 0);
        public static IEnumerable<(int Key, Product Value)> GetDiscountInfo(Dictionary<int, Product> products)
            => products.Where(p => p.Value.Discounts.Count > 0)
            .Select(pair => (pair.Key, pair.Value));
        public static void AddNewProduct(Dictionary<int, Product> products, int productId)
        {
            if (!IsProductAvailable(products, productId))
            {
                Console.Write("Enter the product name: ");
                string productName = Console.ReadLine();
                Console.Write("Enter product price and price type (per kg/per unit) on the same line separated by a space: ");
                string[] userInput = Console.ReadLine().Split(' ');
                decimal price;
                while (!(decimal.TryParse(userInput[0], out price)
                    && userInput.Length != 3
                    && (string.Compare($"{userInput[1]} {userInput[2]}", "Per unit", StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare($"{userInput[1]} {userInput[2]}", "Per kg", StringComparison.OrdinalIgnoreCase) == 0)))
                {
                    Console.Write("Enter product price and price type (per kg/per unit) on the same line separated by a space: ");
                    userInput = Console.ReadLine().Split(' ');
                }
                //decimal productPrice = price;
                Product p = new Product(productName, price, $"{userInput[1]} {userInput[2]}");
                products.Add(productId, p);
                Console.WriteLine("Added the product to the system.");
            }
            else
                Console.WriteLine($"The ID {productId} already exists in the system.");
        }
        public static void RemoveProduct(Dictionary<int, Product> products, int productId)
        {
            if (IsProductAvailable(products, productId))
                products.Remove(productId);
            else
                Console.WriteLine($"The ID {productId} does not exist in the system.");
        }
        public static void ChangePrice(Dictionary<int, Product> products, int productId)
        {
            if (IsProductAvailable(products, productId))
            {
                while (true)
                {
                    Console.Write($"Enter a new price for the product {products[productId].ProductName}: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal unitPrice) && unitPrice > 0)
                    {
                        products[productId].UnitPrice = unitPrice;
                        break;
                    }
                    else
                        Console.WriteLine("Please enter a price value over 0.");
                }
            }
            else
                Console.WriteLine("The product does not exist.");
        }
        public static void ChangeName(Dictionary<int, Product> products, int productId)
        {
            if (IsProductAvailable(products, productId))
            {
                Console.Write($"Enter a new name for the product {products[productId].ProductName}: ");
                string newName = Console.ReadLine();
                products[productId].ProductName = newName;
            }
            else
                Console.WriteLine("The product does not exist.");
        }
        public static bool IsProductAvailable(Dictionary<int, Product> products, int productId) => products.ContainsKey(productId);
        public void AddDiscount(Discount d) => _discount.Add(d);
        public static void DisplayProducts(Dictionary<int, Product> products) => products.ToList().ForEach(p => Console.WriteLine(p.ToString()));
        public override string ToString() => $"Name: {ProductName}, Price: {UnitPrice:C2} {PriceType}";
    }
}