using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.File_IO;
using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Models;
using KassaSystemet.Strategy;
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
    enum SaveFormatEnum
    {
        CSV = 1,
        JSON
    }
    public class AdminMenu : IMenuHandler
    {
        public AdminMenu(IFileManager strategy, IUserInputHandler userInputHandler)
        {
            _fileManagerStrategy = strategy;
            _userInputHandler = userInputHandler;
            adminMenuHandler ??= new AdminMenuHandler();
        }
        private static AdminMenuHandler adminMenuHandler;
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
        private Dictionary<SaveFormatEnum, string> _saveMenu = new Dictionary<SaveFormatEnum, string>()
        {
            {SaveFormatEnum.CSV, "CSV format" },
            {SaveFormatEnum.JSON, "JSON format" },
        };
        public void InitializeMenu()
        {
            AdminMenuEnum userInput;
            do
            {
                DisplayMenu();
                userInput = _userInputHandler.GetAdminMenuEnum();
                bool isChanged = adminMenuHandler.HandleAdminMenuOption(userInput, _userInputHandler);
                if (isChanged)
                {
                    if (_fileManagerStrategy is ISave)
                    {
                        var temp = _fileManagerStrategy as ISave;
                        GetSaveFormat(temp);
                    }
                    _fileManagerStrategy.SaveProductCatalogueToFile(ProductCatalogue.Instance.Products);
                    _fileManagerStrategy.SaveDiscountListToFile(ProductCatalogue.Instance.Products);
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
        public void DisplaySaveMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option below.");
            foreach (var item in _saveMenu)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }
        private static void GetSaveFormat(ISave temp)
        {
            Console.Write("Choose save format:");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                temp = new SaveFileToCSV();
            }
            else if (userInput == "2")
            {
                temp = new SaveFileToJson();
            }
            temp.SaveProductCatalogueToFile(ProductCatalogue.Instance.Products);
        }
    }
}