using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Strategy;

namespace KassaSystemet
{
    public static class App
    {
        private static readonly FileManager _fileManager;
        static App()
        {
            IFileManagerStrategy strategy = new FileManagerStrategy();
            _fileManager = new FileManager(strategy);
        }
        public static void Run()
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 50;
            StartApp();
        }
        private static void StartApp()
        {
            FileManagerOperations.CreateFolders();
            _fileManager.LoadDiscountList();
            Menu.MainMenu(_fileManager);
        }
        public static void CloseApp()
        {
            _fileManager.SaveProductList();
            _fileManager.SaveDiscountList();
            Environment.Exit(0);
        }
    }
}