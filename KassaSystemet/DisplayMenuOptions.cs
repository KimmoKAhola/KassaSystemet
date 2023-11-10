using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class DisplayMenuOptions : IMenuDisplay
    {
        public static void Display()
        {
            Console.Clear();
            Console.WriteLine("***Menu for the cash register***");
            Console.WriteLine("Choose an option below.");
            Console.WriteLine("1. New customer");
            Console.WriteLine("2. Admin tools");
            Console.WriteLine("0. Save & Exit.");
            Console.Write("Enter your command: ");
        }
    }
}
