using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.File_IO;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Utilities;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Models;

namespace KassaSystemet.Menus.MenuPages
{
    public class StartMenu : IMenu
    {

        IFileManager _fileManager;
        IMenuHandler<StartMenuEnum> _appHandler;
        IUserInputHandler _userInputHandler;
        public StartMenu(IFileManager fileManager, IMenuHandler<StartMenuEnum> appHandler, IUserInputHandler userInputHandler)
        {
            _fileManager = fileManager;
            _appHandler = appHandler;
            _userInputHandler = userInputHandler;
        }

        private Dictionary<StartMenuEnum, string> _startMenu = new Dictionary<StartMenuEnum, string>()
        {
            {StartMenuEnum.CustomerMenu, "Customer Menu." },
            {StartMenuEnum.AdminMenu, "Admin Menu." },
            {StartMenuEnum.InfoMenu, "Info Menu." },
            {StartMenuEnum.CreditMenu, "Credits Menu." },
            {StartMenuEnum.Exit, "Save & Exit." },
        };

        public void DisplayMenu()
        {
            Console.Clear();
            PrintMessage("Choose an option below.");
            foreach (var item in _startMenu)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }
        public void InitializeMenu()
        {
            try
            {
                FileManagerOperations.CreateFolders();
                _fileManager.LoadProductListFromFile();
                _fileManager.LoadDiscountListFromFile();
            }
            catch (Exception ex)
            {
                PrintMessage(ex.Message);
            }
            StartMenuEnum userInput;
            do
            {
                DisplayMenu();
                userInput = _userInputHandler.GetMenuEnum<StartMenuEnum>();
                _appHandler.HandleMenuOption(userInput);
            } while (true);
        }
        private static void PrintMessage(string message) => Console.WriteLine(message);
    }
}