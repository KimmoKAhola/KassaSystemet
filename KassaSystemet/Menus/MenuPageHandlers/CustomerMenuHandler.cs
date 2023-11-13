using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Models;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Menus.MenuPages;
using KassaSystemet.File_IO;

namespace KassaSystemet.Menus.MenuPageHandlers
{
    public class CustomerMenuHandler : IMenuHandler<CustomerMenuEnum>
    {
        private ShoppingCart _shoppingCart;
        private IUserInputHandler _userInputHandler;
        IFileManager _fileManager;
        public CustomerMenuHandler(IFileManager fileManager, IUserInputHandler userInputHandler)
        {
            _userInputHandler = userInputHandler;
            _fileManager = fileManager;
            if (_shoppingCart == null || _shoppingCart.Purchases.Count == 0)
                _shoppingCart = new ShoppingCart();
        }

        public void HandleCustomerMenuOption(CustomerMenuEnum userInput)
        {
            switch (userInput)
            {
                case CustomerMenuEnum.First:
                    _shoppingCart.DisplayPurchases();
                    break;
                case CustomerMenuEnum.Second:
                    _shoppingCart.AddProductToCart(_userInputHandler);
                    break;
                case CustomerMenuEnum.Third:
                    ProductCatalogue.Instance.DisplayProducts();
                    break;
                case CustomerMenuEnum.Pay:
                    if (_shoppingCart.Purchases.Count > 0)
                    {
                        string receipt = _shoppingCart.Pay();
                        _fileManager.SaveReceiptToFile(receipt);
                        if (_fileManager is ISave)
                        {
                            GetSaveFormat(_fileManager, _userInputHandler, receipt);
                        }
                    }
                    else
                        Console.WriteLine("Your shopping cart is empty.");
                    break;
                case CustomerMenuEnum.Exit:
                    Console.WriteLine("Returning to the main menu.");
                    break;
                default:
                    Console.WriteLine("Invalid input.", Console.ForegroundColor = ConsoleColor.Red);
                    Thread.Sleep(1000);
                    Console.ResetColor();
                    break;
            }
            Console.ResetColor();
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
        }

        public void HandleMenuOption(CustomerMenuEnum menuOption)
        {
            if (menuOption is CustomerMenuEnum customerMenuEnum)
                HandleCustomerMenuOption(customerMenuEnum);
        }
        private static void GetSaveFormat(ISave temp, IUserInputHandler userInputHandler, string receipt)
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
            }
            temp.SaveReceiptToFile(receipt);

            Thread.Sleep(2000);
        }
    }
}
