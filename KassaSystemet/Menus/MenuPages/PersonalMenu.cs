using KassaSystemet.Interfaces;
using KassaSystemet.File_IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Menus.MenuPages
{
    public class PersonalMenu : IMenu
    {
        private IFileManager _fileManager;
        public PersonalMenu(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public void DisplayMenu()
        {
            var info = LoadInfo(_fileManager);
            PrintMessage(info);
        }
        private static string LoadInfo(IFileManager fileManager)
        {
            return fileManager.LoadPersonalMenuFromFile();
        }
        public void InitializeMenu()
        {
            Console.Clear();
            DisplayMenu();
        }

        private static void PrintMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
