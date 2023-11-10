
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPages;
using KassaSystemet.Strategy;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileManager fileManager = new FileManager(new FileManagerStrategy());
            MenuFactory menuFactory = new MenuFactory(fileManager);
            ModelFactory modelFactory = new ModelFactory();
            var myApp = new App(menuFactory);
            myApp.Run();
        }
    }
}