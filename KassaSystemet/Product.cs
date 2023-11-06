namespace KassaSystemet
{
    public class Product
    {
        public Product(string productName, decimal unitPrice, string priceType)
        {
            ProductName = productName;
            _unitPrice = unitPrice;
            PriceType = priceType.ToLower();
            _discount = new();
        }
        private List<Discount> _discount;
        private decimal _unitPrice;
        public string ProductName { get; set; }
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value > 0)
                    _unitPrice = value;
                else
                    Console.WriteLine("The price can not be set to a negative value.");
            }
        }
        public string PriceType { get; }
        public List<Discount> Discounts => _discount;
        public static void DisplayAllDiscounts()
        {
            var discountedProducts = ProductCatalogue.GetAllDiscounts();

            discountedProducts.ToList().ForEach(product =>
            {
                Console.WriteLine($"The product: {product.ProductName} has the following discounts: ");
                product.Discounts.ForEach(discount => Console.WriteLine(discount.ToString()));
            });
        }
        public static IEnumerable<(int Key, Product Value)> GetDiscountInfo(Dictionary<int, Product> products)
            => products.Where(p => p.Value.Discounts.Count > 0)
            .Select(pair => (pair.Key, pair.Value));
        public void ChangePrice()
        {
            Console.Write("Enter a new price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            UnitPrice = price;
        }
        public void ChangeName()
        {
            Console.Write("Enter a new name: ");
            string name = Console.ReadLine();
            ProductName = name;
        }
        public void AddDiscount(Discount d) => _discount.Add(d);
        public override string ToString() => $"Name: {ProductName}, Price: {UnitPrice:C2} {PriceType}";
    }
}