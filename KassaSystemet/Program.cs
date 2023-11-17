
using Autofac;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.File_IO;
using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.File_IO;
using KassaSystemet.Utilities;
using KassaSystemet.Models;
using KassaSystemet.Container;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ////IUserInputHandler userInputHandler = new UserInputHandler();
            ////IFileManager fileManager = new DefaultFileManager();
            ////MenuFactory menuFactory = new(fileManager, userInputHandler);
            ////AppHandler appHandler = new(menuFactory, userInputHandler);

            ////var app = new App(fileManager, appHandler);
            ////app.StartApp();




            var builder = ContainerConfig.Configure();
            using (var scope = builder.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.StartApp();
            }
        }
    }
}