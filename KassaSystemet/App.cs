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
    public class App : IApplication
    {

        IFileManager _fileManager;
        AppHandler _appHandler;
        public App(IFileManager fileManager, AppHandler appHandler)
        {
            _fileManager = fileManager;
            _appHandler = appHandler;
        }

        public void StartApp()
        {
            FileManagerOperations.CreateFolders();
            _fileManager.LoadProductListFromFile();
            _fileManager.LoadDiscountListFromFile();
            _appHandler.InitializeMenu();
        }
    }
}