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

        public Product(string productName, decimal unitPrice) // add price type (per kg or per piece later)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
            //PriceType = priceType;
        }

        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; }

        public static void AddNewProduct(Dictionary<int, Product> dictionary, int productID, string productName, decimal unitPrice)
        {
            // if (dictionary.ContainsKey(product.ProductID)) Kolla så att produkten samt ID ej finns i systemet.   
            // TODO Lägg till felhantering
            if (!dictionary.ContainsKey(productID))
            {
                dictionary.Add(productID, new Product(productName, unitPrice));
                Console.WriteLine($"Added the product {productName} with id [{productID}] and price {unitPrice} to the system.");
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
            return -50; // If it does not exist return -50. Error handling should be implemented later.
        }
        public static decimal FindProductPrice(Dictionary<int, Product> productDictionary, string productName)
        {
            // Use this to find a certain unit price given a product name.
            // TODO lägg till felhantering

            int productID = GetProductID(productDictionary, productName);
            return Seed.seedDictionary[productID].UnitPrice;
        }

        public static decimal FindProductPrice(Dictionary<int, Product> productDictionary, int productID)
        {
            return Seed.seedDictionary[productID].UnitPrice;
        }
        public static void ChangeProductPrice(Dictionary<int, Product> dictionary, int productID, decimal newPrice)
        {
            // Ändra pris på varan.
            //TODO lägg till felhantering
            Seed.seedDictionary[productID].UnitPrice = newPrice;
        }

        public static void ChangeProductName(Dictionary<int, Product> dictionary, string oldName, string newName)
        {
            if (dictionary.ContainsKey(GetProductID(dictionary, oldName)))
            {
                int productID = GetProductID(dictionary, oldName);
                Seed.seedDictionary[productID].ProductName = newName;
                Console.WriteLine($"The product with old name [{oldName}] has been changed into [{newName}]");
            }
            else
            {
                Console.WriteLine($"The product {oldName} does not exist in the system");
            }
        }

        public static void DisplayProducts(Dictionary<int, Product> dictionary)
        {
            Console.WriteLine("\n\nProduct ID\tProduct Name\tUnit price");
            foreach (var item in dictionary)
            {
                Console.Write($"{item.Key}\t{item.Value.ProductName}\t{item.Value.UnitPrice}\n");
            }
        }

    }
}
