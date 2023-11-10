using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Strategy;
using KassaSystemet.MenuPages;
using KassaSystemet.Factories.MenuFactory;

namespace KassaSystemet
{
    public static class App
    {
        private static readonly FileManager _fileManager;
        private static readonly MenuFactory _menuFactory;

        static App()
        {
            IFileManagerStrategy strategy = new FileManagerStrategy();
            _fileManager = new FileManager(strategy);
            _menuFactory = new MenuFactory();
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
            MenuFactory factory = new MenuFactory();
            StartMenu startMenu = new StartMenu(factory);
            startMenu.Start(_fileManager);
            //StartMenu.MainMenu(_fileManager);
            //StartMenu.Instance.Start();
            //_menuFactory.CreateMenu();
        }
        public static void CloseApp()
        {
            _fileManager.SaveProductList();
            _fileManager.SaveDiscountList();
            Environment.Exit(0);
        }
    }
}