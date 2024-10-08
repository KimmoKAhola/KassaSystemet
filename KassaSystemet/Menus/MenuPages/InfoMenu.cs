﻿using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Menus.MenuPages
{
    public class InfoMenu : IMenu
    {
        public InfoMenu(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        private IFileManager _fileManager;
        public void DisplayMenu()
        {
            var info = LoadInfo(_fileManager);
            PrintMessage(info);
        }

        public void InitializeMenu()
        {
            Console.Clear();
            DisplayMenu();
        }

        private static string LoadInfo(IFileManager fileManager)
        {
            return fileManager.LoadInfoMenuFromFile();
        }
        private static void PrintMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
