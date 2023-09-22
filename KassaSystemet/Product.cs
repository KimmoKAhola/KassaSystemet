using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Product
    {
        /*  Class for products
            Create methods for adding products, removing product, changing price etc
            Admin method in menu class should call on this class.
        */
        //public static Dictionary<int, Product> productDictionary = Seed.seedDictionary;
        //TODO this is only used for seeding purposes
        public static Dictionary<int, Product> productDictionary = new(){ //TODO names are shortened because of formatting. Change later
            { 300, new Product("Bananer", 19.50m, "per kg") },
            { 301, new Product("Äpplen", 25.99m, "per kg") },
            { 302, new Product("Choklad", 13.37m, "per unit") },
            { 303, new Product("Pepsi", 30.50m, "per unit") },
            { 304, new Product("Kexchok", 18.99m, "per unit") },
            { 305, new Product("Sallad", 27.50m, "per kg") },
            { 306, new Product("Jordgub", 5.00m, "per kg") },
            { 307, new Product("Nutella", 21.00m, "per unit") },
            { 308, new Product("Toapapp", 7.00m, "per unit") },
            { 309, new Product("Saffran", 5.50m, "per unit") },
            { 310, new Product("Vatten", 100.00m, "per unit") }};
        public Product(string productName, decimal unitPrice, string priceType) // add price type (per kg or per piece later)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
            PriceType = priceType;
        }

        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; }
        public static void AddNewProduct(Dictionary<int, Product> dictionary, int productID, string productName, decimal unitPrice, decimal discountPrice, string priceType)
        {
            // if (dictionary.ContainsKey(product.ProductID)) Kolla så att produkten samt ID ej finns i systemet.
            // TODO Lägg till felhantering
            if (!dictionary.ContainsKey(productID))
            {
                dictionary.Add(productID, new Product(productName, unitPrice, priceType));
                Console.WriteLine($"Added the product {productName} with id [{productID}] and price {unitPrice} and price type {priceType} to the system." +
                    $"\nDiscount price is: {discountPrice}");
            }
            else
            {
                Console.WriteLine($"The product id {productID} already exists in the system.");
            }
        }
        public static int GetProductID(Dictionary<int, Product> productDictionary, string productName)
        {
            //Want to type in "Bananas" and find its dictionary key.
            foreach (var item in productDictionary)
            {
                if (item.Value.ProductName == productName)
                {
                    return item.Key;
                }
            }
            return -50; //TODO If it does not exist return -50. Error handling should be implemented later.
        }
        public static decimal FindProductPrice(Dictionary<int, Product> productDictionary, string productName)
        {
            // Use this to find a certain unit price given a product name.
            // TODO lägg till felhantering

            int productID = GetProductID(productDictionary, productName);
            return productDictionary[productID].UnitPrice;
        }
        public static string FindProductPriceType(Dictionary<int, Product> productDictionary, string productName)
        {
            int productID = GetProductID(productDictionary, productName);
            return productDictionary[productID].PriceType;
        }
        public static decimal FindProductPrice(Dictionary<int, Product> productDictionary, int productID)
        {
            return productDictionary[productID].UnitPrice;
        }
        public static void ChangeProductPrice(Dictionary<int, Product> dictionary, int productID, decimal newPrice)
        {
            // Ändra pris på varan.
            //TODO lägg till felhantering
            productDictionary[productID].UnitPrice = newPrice;
        }

        public static void ChangeProductName(Dictionary<int, Product> dictionary, string oldName, string newName)
        {
            if (dictionary.ContainsKey(GetProductID(dictionary, oldName)))
            {
                int productID = GetProductID(dictionary, oldName);
                productDictionary[productID].ProductName = newName;
                Console.WriteLine($"The product with old name [{oldName}] has been changed into [{newName}]");
            }
            else
            {
                Console.WriteLine($"The product {oldName} does not exist in the system");
            }
        }

        public static void DisplayProducts(Dictionary<int, Product> dictionary)
        {
            Console.WriteLine("\n\nID\tProduct Name\t\tUnit price\tDiscount price\tPrice type");
            foreach (var item in dictionary)
            {
                Console.WriteLine("Remember to add the discount price from Discount.AllDiscounts!!!");
                Console.Write($"{item.Key}\t{item.Value.ProductName}\t\t\t{item.Value.UnitPrice}\t\t\t\t{item.Value.PriceType}\n");
            }
        }

        public static void CheckForSalesPrice()
        {

            DateTime today = DateTime.Now;
            DateTime start = new DateTime(2023, 9, 19);
            DateTime end = new DateTime(2023, 9, 25);
            DateTime.Compare(start, today); // returns -1 = is false
            DateTime.Compare(end, today); // returns 1 = is true
            //TODO Implement a function which checks if a product is on sale. Should check if current date is between sales date, then check products?
        }

        public static string CreateProductString(Dictionary<int, Product> productDictionary)
        {
            string formattedProductListString = "";
            foreach (var item in productDictionary)
            {
                formattedProductListString += item.Key + "!" + item.Value.ProductName + "!" + item.Value.UnitPrice + "!" + item.Value.PriceType+"!";
            }
            return formattedProductListString.Remove(formattedProductListString.Length-1);
        }
    }
}
