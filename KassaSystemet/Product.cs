using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    /// <summary>
    /// Product class. Handles the product list.
    /// Contains information about product name
    /// product prices and price types.
    /// </summary>
    public class Product
    {
        /*  Class for products
            Create methods for adding products, removing product, changing price etc
            Admin method in menu class should call on this class.
        */
        public static Dictionary<int, Product> productDictionary = new() { };
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
            string formattedString = "";
            formattedString += ($"{"ID",-15} {"Name",-45} {"Price",-20}{"Price type",-10}\n");
            if (IsProductListEmpty())
            {
                Console.WriteLine("There are no products available in the system.");
            }
            else
            {
                foreach (var item in dictionary)
                {
                    formattedString += ($"{item.Key,-16}{item.Value.ProductName,-46}{item.Value.UnitPrice,-20:C2}{item.Value.PriceType,-10}\n");
                }
                Console.WriteLine(formattedString);
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
            if (productDictionary.Count == 0)
            {
                return "";
            }
            else
            {
                //Sorts the dictionary by its key value.
                productDictionary = productDictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                string formattedProductListString = "";
                foreach (var item in productDictionary)
                {
                    formattedProductListString += item.Key + "!" + item.Value.ProductName + "!" + item.Value.UnitPrice + "!" + item.Value.PriceType + "!";
                }
                return formattedProductListString.Remove(formattedProductListString.Length - 1);
            }
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

        public static void RemoveProduct(int productID)
        {
            if (!productDictionary.ContainsKey(productID))
            {
                Console.WriteLine($"The product list does not contain the item with product ID [{productID}]");
            }
            else
            {
                productDictionary.Remove(productID);
            }
        }
    }
}
