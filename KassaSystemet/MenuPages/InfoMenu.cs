using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPages
{
    public class InfoMenu : IMenuHandler
    {
        public void DisplayMenu()
        {
            Console.WriteLine("Info menu.");
        }

        public void InitializeMenu()
        {
            DisplayMenu();
        }
    }
}
