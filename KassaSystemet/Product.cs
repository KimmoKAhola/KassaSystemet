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

        public static void AddNewProduct(Dictionary<int, Product> dictionary, Product product)
        {
            // if (dictionary.ContainsKey(product.ProductID)) Kolla så att produkten samt ID ej finns i systemet.   
            // TODO Lägg till felhantering
            //dictionary.Add(, product);
            Console.WriteLine("Product added!");
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
            return -50;
        }
        public static decimal FindProductPrice(Dictionary<int, Product> dictionary)
        {
            // Use this to find a certain unit price given a product ID.
            // TODO lägg till felhantering
            foreach (var product in dictionary)
            {
                    Console.WriteLine("dictionary key: " + product.Key);
                    return product.Value.UnitPrice; // The unit price for a given product
            }
            return -1;
        }
        public static void ChangeProductPrice(Dictionary<int, Product> dictionary, int productID, decimal newPrice)
        {
            // Ändra pris på varan. Använd FindProduct med produkt id och ändra priset.
            //TODO lägg till felhantering
            dictionary[productID].UnitPrice = newPrice;
        }

        public static void DisplayProducts(Dictionary<int, Product> dictionary)
        {
            Console.WriteLine("\n\nProduct ID\tProduct Name\tUnit price");
            foreach (var item in dictionary)
            {
                Console.Write($"{item.Key}\t{item.Value.ProductName}\t{item.Value.UnitPrice}\n");
            }
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string GetProductFilePath()
        {
            return $"../../../Files/PRODUCTLIST_{GetCurrentDate()}.txt"; // TODO Check if this can be made nicer
        }

        public static void SaveToFile(Dictionary<int, Product> dictionary)
        {
            using (StreamWriter streamWriter = new(GetProductFilePath(), append: true))
            {
                streamWriter.Write("Product ID\tProduct Name\tUnit price\n"); // TODO Fix formatting in the future
                foreach (var item in dictionary)
                {
                    streamWriter.Write($"{item.Key}\t{item.Value.ProductName}\t{item.Value.UnitPrice}\n");
                }
            }
        }

        
    }
}
