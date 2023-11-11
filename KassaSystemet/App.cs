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
        private MenuFactory _menuFactory;
        public App(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
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
            StartMenu _startMenu = new StartMenu(_menuFactory);
            _startMenu.Start();
        }
    }
}