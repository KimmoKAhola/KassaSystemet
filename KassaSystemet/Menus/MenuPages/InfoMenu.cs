using KassaSystemet.Interfaces;
using KassaSystemet.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Menus.MenuPages
{
    public class InfoMenu : IMenu
    {
        private IFileManager _fileManager;
        public InfoMenu(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public void DisplayMenu()
        {
            Console.Clear();
            var info = LoadInfo(_fileManager);
            Console.WriteLine(info);
            Console.ReadKey();
        }
        private static string LoadInfo(IFileManager fileManager)
        {
            return fileManager.LoadInfoMenuFromFile();
        }
        public void InitializeMenu()
        {
            DisplayMenu();
        }
    }
}
