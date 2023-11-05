﻿
namespace KassaSystemet
{
    public static class ProductDataBase
    {
        public static readonly string _wares =
            "300!Bananer!15,50!per kg!" +
            "301!Äpplen!25,50!per kg!" +
            "302!Kaffe!65,50!per unit!" +
            "303!Choklad!19,90!per unit!" +
            "304!Lösgodis!89,90!per kg!" +
            "305!Rågbröd!55,00!per unit!" +
            "306!Toalettpapper!32,00!per unit!" +
            "307!Kex!25,60!per unit!" +
            "308!Vattenmelon!55,00!per kg!" +
            "309!Smör!79,00!per kg!" +
            "310!Gott & Blandat!29,00!per unit!" +
            "311!Hushållsost!79,00!per kg!" +
            "312!Kycklingfilé!119,00!per kg!" +
            "313!Yoggi!40,00!per unit!" +
            "314!Tomater på burk!11,00!per unit!" +
            "315!Stekpanna!339,00!per unit!" +
            "316!Dammsugare!999,99!per unit!" +
            "317!Västerbottensost!10,00!per kg!" +
            "318!Oxfilé!399,99!per kg!" +
            "319!Päron!35,99!per kg!" +
            "320!Pasta!19,99!per unit!";
        public static Dictionary<int, Product> SeedProducts()
        {
            Dictionary<int, Product> productDatabase = new();
            string[] products = _wares.Split('!');

            for (int i = 0; i < products.Length - 1; i += 4)
            {
                int id = Convert.ToInt32(products[i]);
                string name = products[i + 1].Trim();
                decimal price = Convert.ToDecimal(products[i + 2]);
                string type = products[i + 3];
                Product p = new Product(name, price, type);
                productDatabase.Add(id, p);
            }
            return productDatabase;
        }
        public static void DisplayProducts(Dictionary<int, Product> products)
        {
            foreach (var item in products)
            {
                Console.Write($"Product ID: {item.Key}, {item.Value}");
            }
        }
    }
}
