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
            { 304, new Product("Kexchoklaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaad", 18.99m, "per unit") },
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
            PriceType = priceType.ToLower();
        }

        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; }

        public static bool DoesProductExist(int productID)
        {
            if (productDictionary.ContainsKey(productID))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Your product with id [{productID}] does not exist in the system.");
                return false;
            }
        }
        public static bool IsProductListEmpty()
        {
            if (productDictionary.Count == 0)
            {
                return true;
            }
            else
                return false;
        }
        public static void AddNewProduct(Dictionary<int, Product> dictionary, int productID, string productName, decimal unitPrice, string priceType)
        {
            if (!dictionary.ContainsKey(productID))
            {
                dictionary.Add(productID, new Product(productName, unitPrice, priceType));
                Console.WriteLine($"Added the product {productName} with id [{productID}] and price {unitPrice} and price type {priceType} to the system.");
            }
            else
            {
                Console.WriteLine($"The product id {productID} already exists in the system.");
            }
        }
        public static int GetProductID(Dictionary<int, Product> productDictionary, int productID)
        {
            //Want to type in ProductID and find its dictionary key.
            foreach (var item in productDictionary)
            {
                if (item.Key == productID)
                {
                    return item.Key;
                }
            }
            return -50; //TODO If it does not exist return -50. Error handling should be implemented later.
        }

        public static int GetProductID(Dictionary<int, Product> productDictionary, string productName)
        {
            foreach (var item in productDictionary)
            {
                if (item.Value.ProductName == productName)
                {
                    return item.Key;
                }
            }
            return -50;
        }
        public static decimal FindProductPrice(Dictionary<int, Product> productDictionary, int productID)
        {
            if (productDictionary.ContainsKey(productID))
            {
                return productDictionary[productID].UnitPrice;
            }
            else
            {
                return -110m;
            }
        }

        public static string FindProductPriceType(Dictionary<int, Product> productDictionary, int productID)
        {
            return productDictionary[productID].PriceType;
        }

        public static string GetProductName(Dictionary<int, Product> productDictionary, int productID)
        {
            return productDictionary[productID].ProductName;
        }
        public static void ChangeProductPrice(Dictionary<int, Product> dictionary, int productID, decimal newPrice)
        {
            if (dictionary.ContainsKey(GetProductID(dictionary, productID)) && newPrice > 0m)
            {
                productDictionary[productID].UnitPrice = newPrice;
            }
            else if (newPrice <= 0m)
            {
                Console.WriteLine($"The price {newPrice} value is invalid. The price can not be lower or equal to 0.");
            }
            else
            {
                Console.WriteLine($"The product with ID [{productID}] does not exist in the system.");
            }
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
                Console.WriteLine($"The product with name [{oldName}] does not exist in the system.");
            }
        }

        public static void DisplayProducts(Dictionary<int, Product> dictionary)
        {
            if (IsProductListEmpty())
            {
                Console.WriteLine("Your product list is currently empty.");
            }
            else
            {
                Console.WriteLine("\n\nID\tProduct Name\t\tUnit price\t\t\tPrice type");
                foreach (var item in dictionary)
                {
                    Console.Write($"{item.Key}\t{item.Value.ProductName}\t\t\t{item.Value.UnitPrice}\t\t\t\t{item.Value.PriceType}\n");
                }
            }
        }

        public static void CheckForSalesPrice()
        {
            DateTime today = DateTime.Now;
            DateTime start = new(2023, 9, 19);
            DateTime end = new(2023, 9, 25);
            DateTime.Compare(start, today); // returns -1 = is false
            DateTime.Compare(end, today); // returns 1 = is true
            //TODO Implement a function which checks if a product is on sale. Should check if current date is between sales date, then check products?
        }

        public static string CreateProductString(Dictionary<int, Product> productDictionary)
        {
            string formattedProductListString = "";
            foreach (var item in productDictionary)
            {
                formattedProductListString += item.Key + "!" + item.Value.ProductName + "!" + item.Value.UnitPrice + "!" + item.Value.PriceType + "!";
            }
            return formattedProductListString.Remove(formattedProductListString.Length - 1);
        }

        /// <summary>
        /// Returns true if priceType is per kg.
        /// Returns false if priceType is per piece.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool CheckPriceType(int productID)
        {
            if (productDictionary.ContainsKey(productID) && FindProductPriceType(productDictionary, productID) == "per kg")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
