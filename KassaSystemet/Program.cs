
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.File_IO;
using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Strategy;
using KassaSystemet.Utilities;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IFileManager fileManager = new FileManager(new DefaultFileManager());

            //IUserInputHandler userInputHandler = new UserInputHandler();

            //MenuFactory menuFactory = new MenuFactory(fileManager, userInputHandler);

            //var myApp = new App(menuFactory, fileManager);
            var myApp = new App();
            myApp.StartApp();
        }
    }
}