
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPages;
using KassaSystemet.Strategy;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileManager manager = new FileManager(new FileManagerStrategy());
            MenuFactory factory = new MenuFactory(manager);
            var myApp = new App(factory);
            myApp.Run();
        }
    }
}