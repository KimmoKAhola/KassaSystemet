using Autofac;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.File_IO;
using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Menus.MenuPages;
using KassaSystemet.Models;
using KassaSystemet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Container
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            RegisterMenus(builder);
            RegisterHandlers(builder);
            RegisterMenuHandlers(builder);
            builder.RegisterType<App>().As<IApplication>();

            return builder.Build();
        }

        private static void RegisterHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultFileManager>().As<IFileManager>();
            builder.RegisterType<UserInputHandler>().As<IUserInputHandler>();
        }
        private static void RegisterMenuHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<AdminMenuHandler>().As<IMenuHandler<AdminMenuEnum>>();
            builder.RegisterType<CustomerMenuHandler>().As<IMenuHandler<CustomerMenuEnum>>();
            builder.RegisterType<AppHandler>().SingleInstance();
        }
        private static void RegisterMenus(ContainerBuilder builder)
        {
            builder.RegisterType<MenuFactory>().AsSelf();
            builder.RegisterType<AdminMenu>().SingleInstance();
            builder.RegisterType<CustomerMenu>().SingleInstance();
        }
    }
}
