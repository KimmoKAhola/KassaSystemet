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
        public App()
        {
        }
        public void StartApp()
        {
            IFileManager fileManager = new FileManager(new DefaultFileManager());
            IUserInputHandler userInputHandler = new UserInputHandler();
            MenuFactory menuFactory = new MenuFactory(fileManager, userInputHandler);
            FileManagerOperations.CreateFolders();
            fileManager.LoadDiscountListFromFile();

            AppHandler startMenuOptions = new AppHandler(menuFactory);
            startMenuOptions.InitializeMenu();
        }
    }
}