using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class App
    {
        public static void Run()
        {
            InitializeSystem();
            Menu.MainMenu();
        }
        private static void InitializeSystem()
        {
            FileManager.CreateFolders();
            var products = ProductCatalogue.Instance;
            FileManager.LoadDiscountList(products.Products);
        }
        public static void CloseApp(Dictionary<int, Product> products)
        {
            FileManager.CreateFolders();
            FileManager.SaveProductList(products);
            FileManager.SaveDiscountList(products);
            Environment.Exit(0);
        }
    }
}