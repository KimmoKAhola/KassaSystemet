using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.File_IO;
using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Menus.MenuPages
{
    public enum AdminMenuEnum
    {
        First = 1,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eighth,
        Ninth,
        Exit
    }
    public enum SaveFormatEnum
    {
        CSV = 1,
        JSON,
        BINARY
    }
    public class AdminMenu : IMenu
    {
        public AdminMenu(IFileManager strategy, IUserInputHandler userInputHandler)
        {
            _fileManagerStrategy = strategy;
            _userInputHandler = userInputHandler;
            _adminMenuHandler ??= new AdminMenuHandler(userInputHandler);
        }
        private static AdminMenuHandler _adminMenuHandler;
        private IFileManager _fileManagerStrategy;
        private IUserInputHandler _userInputHandler;
        private Dictionary<AdminMenuEnum, string> _adminMenu = new Dictionary<AdminMenuEnum, string>()
        {
            {AdminMenuEnum.First, "Add a new product to the system." },
            {AdminMenuEnum.Second, "Display available products in the system." },
            {AdminMenuEnum.Third, "Change a product's price." },
            {AdminMenuEnum.Fourth, "Change a product's name." },
            {AdminMenuEnum.Fifth, "Add a new discount to the system." },
            {AdminMenuEnum.Sixth, "Display all available discounts for a specific product." },
            {AdminMenuEnum.Seventh, "Display all available discounts in the system." },
            {AdminMenuEnum.Eighth, "Remove a specific discount from a product." },
            {AdminMenuEnum.Ninth, "Remove a product from the system." },
            {AdminMenuEnum.Exit, "Return to the main menu." },
        };
        public static Dictionary<SaveFormatEnum, string> _saveMenu = new Dictionary<SaveFormatEnum, string>()
        {
            {SaveFormatEnum.CSV, "CSV format" },
            {SaveFormatEnum.JSON, "JSON format" },
            {SaveFormatEnum.BINARY, "Binary format" }
        };
        public void InitializeMenu()
        {
            AdminMenuEnum userInput;
            do
            {
                DisplayMenu();
                userInput = _userInputHandler.GetMenuEnum<AdminMenuEnum>();
                bool isChanged = _adminMenuHandler.HandleAdminMenuOption(userInput);
                if (isChanged)
                {
                    if (_fileManagerStrategy is ISave)
                    {
                        var temp = _fileManagerStrategy as ISave;
                        DisplaySaveMenu();
                        GetSaveFormat(temp, _userInputHandler);
                    }
                    _fileManagerStrategy.SaveProductCatalogueToFile();
                    _fileManagerStrategy.SaveDiscountCatalogueToFile();
                }
            } while (userInput != AdminMenuEnum.Exit);
        }
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("**Admin menu**\nChoose an option below.");
            foreach (var item in _adminMenu)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }
        private void DisplaySaveMenu()
        {
            Console.Clear();
            Console.ResetColor();
            PrintSuccessMessage("Your product list has been updated and will be saved. Please choose a file format.\nChoose an option below.");
            foreach (var item in _saveMenu)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }
        private static void GetSaveFormat(ISave temp, IUserInputHandler userInputHandler)
        {
            SaveFormatEnum userInput = userInputHandler.GetMenuEnum<SaveFormatEnum>();

            switch (userInput)
            {
                case SaveFormatEnum.CSV:
                    temp = new SaveFileToCSV();
                    break;
                case SaveFormatEnum.JSON:
                    temp = new SaveFileToJson();
                    break;
                case SaveFormatEnum.BINARY:
                    temp = new SaveFileToBinary();
                    break;
            }
            temp.SaveProductCatalogueToFile();
            temp.SaveDiscountCatalogueToFile();
            PrintSuccessMessage("Product list has been save to the chosen format. Returning to the previous menu...");
            LoadingAnimation();
        }
        private static void PrintSuccessMessage(string message) => Console.WriteLine(message);
        private static void LoadingAnimation()
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Write("-");
                Thread.Sleep(200);
            }
            Console.WriteLine();
        }
    }
}