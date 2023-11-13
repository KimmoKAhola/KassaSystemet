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

            builder.RegisterType<DefaultFileManager>().As<IFileManager>();
            builder.RegisterType<UserInputHandler>().As<IUserInputHandler>();
            builder.RegisterType<MenuFactory>().AsSelf();
            builder.RegisterType<AppHandler>().SingleInstance();
            builder.RegisterType<App>().As<IApplication>();
            builder.RegisterType<AdminMenu>().SingleInstance();
            builder.RegisterType<AdminMenuHandler>().As<IMenuHandler<AdminMenuEnum>>();
            builder.RegisterType<CustomerMenu>().SingleInstance();
            builder.RegisterType<CustomerMenuHandler>().As<IMenuHandler<CustomerMenuEnum>>();

            return builder.Build();
        }
    }
}
