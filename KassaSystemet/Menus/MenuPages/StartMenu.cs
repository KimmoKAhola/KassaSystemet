using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Models;
using KassaSystemet.Strategy;

namespace KassaSystemet.Menus.MenuPages
{
    public enum StartMenuEnum
    {
        First = 1,
        Second,
        Third,
        Exit
    }
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