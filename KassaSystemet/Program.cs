
using Autofac;
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
            var builder = new ContainerBuilder();

            builder.RegisterType<DefaultFileManager>().As<IFileManager>();
            builder.RegisterType<UserInputHandler>().As<IUserInputHandler>();
            builder.RegisterType<MenuFactory>().AsSelf();
            builder.RegisterType<AppHandler>().AsSelf();
            builder.RegisterType<App>().AsSelf();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<App>();
                app.StartApp();
            }
        }
    }
}