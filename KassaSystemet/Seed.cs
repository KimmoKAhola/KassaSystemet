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
        public static Dictionary<int, Product> seedDictionary = new(){ 
            { 300, new Product("Bananer", 19.50m) },
            { 301, new Product("Äpplen", 25.99m) },
            { 302, new Product("Chokladglass", 13.37m) },
            { 303, new Product("Pepsi", 30.50m ) },
            { 304, new Product("Kexchoklad", 18.99m) },
            { 305, new Product("Sallad", 27.50m) },
            { 306, new Product("Jordgubbar", 5.00m) },
            { 307, new Product("Nutella", 21.00m) },
            { 308, new Product("Toapapper", 7.00m) },
            { 309, new Product("Saffran", 5.50m) },
            { 310, new Product("Vatten", 100.00m) }};

        public static List<Purchase> seedProductList = new(){ 
            { new Purchase("Bananer", 10)},
            { new Purchase("Äpplen", 7) },
            { new Purchase("Chokladglass", 1) },
            { new Purchase("Pepsi", 3) },
            { new Purchase("Kexchoklad", 2) },
            { new Purchase("Sallad", 1) },
            { new Purchase("Jordgubbar", 3) },
            { new Purchase("Nutella", 2) },
            { new Purchase("Toapapper", 10) },
            { new Purchase("Saffran", 25) },
            { new Purchase("Vatten", 3) } };
    }
}
