using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPageServices;
using KassaSystemet.Models;
using KassaSystemet.Strategy;

namespace KassaSystemet.MenuPages
{
    public class StartMenu
    {
        private MenuFactory _menuFactory;
        public StartMenu(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public void Start()
        {
            StartMenuHandler startMenuOptions = new StartMenuHandler(_menuFactory);
            startMenuOptions.InitializeMenu();
        }
    }
}