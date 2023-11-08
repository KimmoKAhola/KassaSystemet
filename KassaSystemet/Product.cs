namespace KassaSystemet
{
    public class Product
    {
        public Product(string productName, decimal unitPrice, string priceType)
        {
            ProductName = productName;
            if (productName.Length > 20)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Your product name was too long and has been shortened to {ProductName}");
            }
            UnitPrice = unitPrice;
            PriceType = priceType.ToLower();
            _discount = new();
        }
        private List<Discount> _discount;
        //private decimal _unitPrice;
        private string _productName;
        private int maxProductNameLength = 20;
        public string ProductName
        {
            get => _productName;
            set => _productName = (value.Length > maxProductNameLength) ? value.Substring(0, maxProductNameLength) : value;
        }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; }
        public List<Discount> Discounts => _discount;
        public static IEnumerable<(int Key, Product Value)> GetDiscountForSingleProduct(Dictionary<int, Product> products)
            => products.Where(p => p.Value.Discounts.Count > 0)
            .Select(pair => (pair.Key, pair.Value));
        public void ChangeProductPrice()
        {
            Console.Write("Enter a new price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
            {
                UnitPrice = price;
                Console.WriteLine($"The price has been changed to {price:C2}.");
            }
            else
                Console.WriteLine("Incorrect input. The price has not been changed.");
        }
        public decimal GetBestDiscount() => Discounts.Max(discount => discount.DiscountPercentage);
        public void ChangeProductName()
        {
            Console.Write("Enter a new product name: ");
            string name = Console.ReadLine();
            ProductName = name;
            if (name.Length > maxProductNameLength)
                Console.WriteLine($"Your product name was too long and has been shortened to {name.Substring(0, maxProductNameLength)}");
        }
        public void AddDiscountToProduct(Discount d) => _discount.Add(d);
        public void Display() => Discounts.ForEach(x => Console.WriteLine(x.ToString()));
        public bool HasActiveDiscount()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return Discounts.Any(discount => today >= discount.StartDate && today <= discount.EndDate);
        }
        public override string ToString() => $"Name: {ProductName}, Price: {UnitPrice:C2} {PriceType}";
    }
}