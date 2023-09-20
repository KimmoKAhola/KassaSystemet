using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class Seed
    {
        //This class is used for seeding later. For now it just contains a dictionary with random products.
        //Currently only used for testing the different functions without having to type in anything manually.
        public static Dictionary<int, Product> seedDictionary = new(){ //TODO names are shortened because of formatting. Change later
            { 300, new Product("Bananer", 19.50m, -1m, "per kg") },
            { 301, new Product("Äpplen", 25.99m, -1m, "per kg") },
            { 302, new Product("Choklad", 13.37m, -1m, "per unit") },
            { 303, new Product("Pepsi", 30.50m, -1m, "per unit") },
            { 304, new Product("Kexchok", 18.99m, -1m, "per unit") },
            { 305, new Product("Sallad", 27.50m, -1m, "per kg") },
            { 306, new Product("Jordgub", 5.00m, -1m, "per kg") },
            { 307, new Product("Nutella", 21.00m, -1m, "per unit") },
            { 308, new Product("Toapapp", 7.00m, -1m, "per unit") },
            { 309, new Product("Saffran", 5.50m, -1m, "per unit") },
            { 310, new Product("Vatten", 100.00m, -1m, "per unit") }};
        public static Dictionary<string, decimal> discountDictionary = new() { }; // Key should be product name and it should contain discount price for that product name.
        public static List<Purchase> seedProductList = new(){
            { new Purchase("Bananer", 10)},
            { new Purchase("Äpplen", 7) },
            { new Purchase("Choklad", 1) },
            { new Purchase("Pepsi", 3) },
            { new Purchase("Kexchok", 2) },
            { new Purchase("Sallad", 1) },
            { new Purchase("Jordgub", 3) },
            { new Purchase("Nutella", 2) },
            { new Purchase("Toapapp", 10) },
            { new Purchase("Saffran", 25) },
            { new Purchase("Vatten", 3) } };
    }
}
