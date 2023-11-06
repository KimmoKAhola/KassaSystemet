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
            InitializeSystem();
            Menu.MainMenu();
        }
        private static void InitializeSystem()
        {
            FileManager.CreateFolders();
            var products = ProductCatalogue.Instance;
            FileManager.LoadDiscountList();
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