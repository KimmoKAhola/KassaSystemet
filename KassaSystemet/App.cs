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
            Console.WindowWidth = 150;
            Console.WindowHeight = 50;
            StartApp();
        }
        private static void StartApp()
        {
            FileManager.CreateFolders();
            var products = ProductCatalogue.Instance;
            FileManager.LoadDiscountList();
            Menu.MainMenu();
        }
        public static void CloseApp()
        {
            FileManager.CreateFolders();
            FileManager.SaveProductList();
            FileManager.SaveDiscountList();
            Environment.Exit(0);
        }
    }
}