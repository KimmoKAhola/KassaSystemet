using static System.Net.Mime.MediaTypeNames;

namespace KassaSystemet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManager.LoadProductList();
            Menu menu = new();
            menu.MainMenu();
        }
    }
}