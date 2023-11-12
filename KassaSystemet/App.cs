﻿using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Strategy;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Menus.MenuPages;

namespace KassaSystemet
{
    public class App
    {
        private MenuFactory _menuFactory;
        public App(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }
        public void StartApp()
        {
            FileManagerOperations.CreateFolders();
            StartMenu _startMenu = new StartMenu(_menuFactory);
            _startMenu.Start();
        }
    }
}