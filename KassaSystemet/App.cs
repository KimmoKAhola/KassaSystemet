using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class App
    {
        public static void Run()
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 50;
            LoadApp();
            Console.CursorVisible = true;
            StartApp();
        }
        private static void StartApp()
        {
            FileManager.CreateFolders();
            FileManager.LoadDiscountList();
            Menu.MainMenu();
        }
        public static void CloseApp()
        {
            FileManager.CreateFolders();
            FileManager.SaveProductList();
            FileManager.SaveDiscountList();
            Environment.Exit(0);
        }
        private static void LoadApp()
        {
            Console.WriteLine();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Random random = new Random();
            string blockSize = new string(' ', (150 / 25));
            int totalSteps = 20;
            Console.SetCursorPosition(0, 25);
            for (int i = 0; i < totalSteps; i++)
            {
                string LoadingString = $"Loading {(double)i / (totalSteps - 1):P2} │";
                Console.Write(LoadingString);
                Console.SetCursorPosition(16 + i * blockSize.Length, 25);
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(blockSize);
                Console.SetCursorPosition(16 + 20 * blockSize.Length, 25);
                Console.ResetColor();
                Console.Write('│');
                int sleep = random.Next(500 - i * totalSteps, 1000 - 2 * i * totalSteps);
                Console.SetCursorPosition(0, 25);
                Thread.Sleep(sleep);
                if (i == 18)
                    Thread.Sleep(5000);
            }
            Thread.Sleep(500);
            Console.Clear();
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("App loaded, enjoy your stay!");
            Thread.Sleep(1500);
            Console.SetCursorPosition(0, 0);
        }
    }
}