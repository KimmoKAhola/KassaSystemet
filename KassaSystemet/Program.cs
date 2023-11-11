
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
            GetValidProductPriceType();
            IFileManager fileManager = new FileManager(new FileManagerStrategy());
            MenuFactory menuFactory = new MenuFactory(fileManager);
            ModelFactory modelFactory = new ModelFactory(); //TODO remove?
            var myApp = new App(menuFactory);
            myApp.Run();
        }
        private static string GetValidProductPriceType()
        {
            while (true)
            {
                Console.Write("Enter a product price type (per kg/per unit): ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "per kg" || userInput == "per unit")
                    return userInput;
                else
                    Console.WriteLine("Invalid price type. Please enter 'per kg' or 'per unit'.");
            }
        }
    }
}