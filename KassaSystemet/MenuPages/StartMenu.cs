using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPageServices;
using KassaSystemet.Models;
using KassaSystemet.Strategy;

namespace KassaSystemet.MenuPages
{
    public class StartMenu
    {
        private readonly IFileManager _fileManagerStrategy;
        private readonly MenuFactory _menuFactory;
        public StartMenu(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
            //_fileManagerStrategy = fileManagerStrategy;
        }

        public void Start()
        {
            StartMenuHandler startMenuOptions = new StartMenuHandler(_menuFactory, _fileManagerStrategy);
            startMenuOptions.InitializeMenu();
        }
    }
}