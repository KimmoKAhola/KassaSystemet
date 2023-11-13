using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.File_IO;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Menus.MenuPages;
using KassaSystemet.Utilities;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Models;

namespace KassaSystemet
{
    public class App
    {

        private MenuFactory _menuFactory;
        private IFileManager _fileManager;

        public App(MenuFactory menuFactory, IFileManager fileManager)
        {
            _menuFactory = menuFactory;
            _fileManager = fileManager;
        }

        public void StartApp()
        {
            FileManagerOperations.CreateFolders();
            var test = ProductCatalogue.Instance.Products;
            _fileManager.LoadProductListFromFile();
            _fileManager.LoadDiscountListFromFile();
            AppHandler startMenuOptions = new AppHandler(_menuFactory);
            startMenuOptions.InitializeMenu();
        }
    }
}