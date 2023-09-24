using static System.Net.Mime.MediaTypeNames;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new();
            menu.MainMenu();
        }
    }
}