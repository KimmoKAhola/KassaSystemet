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
                    try
                    {
                        _shoppingCart.AddProductToCart(_userInputHandler);
                    }
                    catch (Exception ex)
                    {
                        PrintErrorMessage($"{ex.Message} (why did you do that?)");
                    }
                    break;
                case CustomerMenuEnum.Third:
                    ProductCatalogue.Instance.DisplayProducts();
                    break;
                case CustomerMenuEnum.Pay:
                    if (_shoppingCart.Purchases.Count > 0)
                    {
                        string receipt = _shoppingCart.Pay();
                        try
                        {
                            _fileManager.SaveReceiptToFile(receipt);
                            if (_fileManager is ISave)
                            {
                                DisplaySaveMenu();
                                GetSaveFormat(_fileManager, _userInputHandler, receipt);
                            }
                        }
                        catch (Exception ex)
                        {
                            PrintErrorMessage(ex.Message);
                        }
                    }
                    else
                        PrintErrorMessage("Your shopping cart is empty.");
                    break;
                case CustomerMenuEnum.Exit:
                    PrintSuccessMessage("Returning to the main menu.");
                    _shoppingCart.Purchases.Clear();
                    break;
                default:
                    PrintErrorMessage("Invalid input.");
                    Thread.Sleep(1000);
                    break;
            }
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
                case SaveFormatEnum.BINARY:
                    temp = new SaveFileToBinary();
                    break;
            }
            temp.SaveReceiptToFile(receipt);
            PrintSuccessMessage("Receipt has been save to the chosen format. Returning to the previous menu...");
            LoadingAnimation();
        }
        private void DisplaySaveMenu()
        {
            PrintSuccessMessage("Your receipt has been saved to a text file. Please choose another file format.\nChoose an option below.");
            foreach (var item in AdminMenu._saveMenu)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }
        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
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
