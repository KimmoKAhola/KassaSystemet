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
            Dictionary<int, Product> products = InitializeSystem();
            Menu.MainMenu(products);
        }
        private static Dictionary<int, Product> InitializeSystem()
        {
            FileManager.CreateFolders();
            Dictionary<int, Product> products = FileManager.LoadProductList();
            FileManager.LoadDiscountList(products);
            return products;
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