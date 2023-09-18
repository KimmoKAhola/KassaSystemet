﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Product
    {
        /*  Dictionary for products
            Create methods for adding products, removing product, changing price etc
            Admin method in menu class should call on this class.
        */
        public Product()
        {
            //Might not be needed
        }
        public Product(string productName, int productID, decimal unitPrice)
        {
            ProductName = productName;
            ProductID = productID;
            UnitPrice = unitPrice;
        }

        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }

        public static void AddNewProduct(Dictionary<int, Product> dictionary, Product product)
        {
            // if (dictionary.ContainsKey(product.ProductID)) Kolla så att produkten samt ID ej finns i systemet.   
            // TODO Lägg till felhantering
            dictionary.Add(product.ProductID, product);
            Console.WriteLine("Product added!");
        }

        public static Product FindProduct(Dictionary<int, Product> dictionary, int productID)
        {
            // Sök mha produkt ID. Anta att produkt finns för tillfället
            // Denna metod ska användas för att hitta produktID baserat på sträng. Användare ska söka efter "Bananer" etc.
            // TODO lägg till felhantering
            return dictionary[productID];
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

        static readonly string date = DateTime.Now.ToShortDateString();
        static readonly string filePath = $"../../../Files/{date}.txt"; // TODO Check if this can be made nicer
        public static void SaveToFile(Dictionary<int, Product> dictionary)
        {
            using (StreamWriter streamWriter = new(filePath, append: true))
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
