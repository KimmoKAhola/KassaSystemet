using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPageServices;
using KassaSystemet.Models;
using KassaSystemet.Strategy;

namespace KassaSystemet.MenuPages
{
    public class StartMenu
    {
        private readonly IFileManagerStrategy _fileManagerStrategy;
        private readonly MenuFactory _menuFactory;
        public StartMenu(MenuFactory menuFactory, IFileManagerStrategy fileManagerStrategy)
        {
            _menuFactory = menuFactory;
            _fileManagerStrategy = fileManagerStrategy;
        }

        public void Start()
        {
            StartMenuOptions startMenuOptions = new StartMenuOptions(_menuFactory, _fileManagerStrategy);
            startMenuOptions.InitializeMenu();
        }
    }
}