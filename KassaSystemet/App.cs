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
    public class App
    {
        private readonly FileManager _fileManager;
        private readonly MenuFactory _menuFactory;
        public App()
        {
            IFileManagerStrategy strategy = new FileManagerStrategy();
            _fileManager = new FileManager(strategy);
            _menuFactory = new MenuFactory();
        }
        public void Run()
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 50;
            StartApp();
        }
        private void StartApp()
        {
            FileManagerOperations.CreateFolders();
            _fileManager.LoadDiscountList();
            StartMenu _startMenu = new StartMenu(_menuFactory);
            _startMenu.Start(_fileManager);
        }
    }
}