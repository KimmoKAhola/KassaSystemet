using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Strategy;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Menus.MenuPages;
using KassaSystemet.Utilities;
using KassaSystemet.Menus.MenuPageHandlers;

namespace KassaSystemet
{
    public class App
    {
        private MenuFactory _menuFactory;
        private IFileManager _fileManager;
        private static AppHandler _startMenuHandler;
        public App(MenuFactory menuFactory, IFileManager fileManager)
        {
            _menuFactory = menuFactory;
            _fileManager = fileManager;
            _startMenuHandler ??= new AppHandler(_menuFactory);
        }

        public void StartApp()
        {
            FileManagerOperations.CreateFolders();
            _fileManager.LoadDiscountListFromFile();

            AppHandler startMenuOptions = new AppHandler(_menuFactory);
            startMenuOptions.InitializeMenu();
        }
        //public void StartApp()
        //{
        //    FileManagerOperations.CreateFolders();
        //    IFileManager fileManager = new FileManager(new DefaultFileManager());
        //    IUserInputHandler userInputHandler = new UserInputHandler();
        //    StartMenu _startMenu = new StartMenu(fileManager, userInputHandler);
        //    _startMenu.InitializeMenu();
        //}
    }
}