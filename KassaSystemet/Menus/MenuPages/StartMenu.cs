//using KassaSystemet.Factories.MenuFactory;
//using KassaSystemet.Interfaces;
//using KassaSystemet.Menus.MenuPageHandlers;
//using KassaSystemet.Models;
//using KassaSystemet.Strategy;
//using KassaSystemet.Utilities;

//namespace KassaSystemet.Menus.MenuPages
//{
//    public enum StartMenuEnum
//    {
//        First = 1,
//        Second,
//        Third,
//        Exit
//    }
//    public class StartMenu
//    {


//        //public StartMenu()
//        //{

//        //}

//        //public void Start()
//        //{

//        //}

//        //private MenuFactory _menuFactory;
//        //private IFileManager _fileManager;
//        //private IUserInputHandler _userInputHandler;
//        //private static StartMenuHandler _startMenuHandler;
//        //public StartMenu(IFileManager fileManager, IUserInputHandler userInputHandler)
//        //{
//        //    _fileManager = fileManager;
//        //    _userInputHandler = userInputHandler;
//        //    _startMenuHandler ??= new StartMenuHandler(_menuFactory);
//        //}
//        //private IMenu _menu;
//        //private Dictionary<StartMenuEnum, string> _startMenu = new Dictionary<StartMenuEnum, string>()
//        //{
//        //    {StartMenuEnum.First, "Customer Menu." },
//        //    {StartMenuEnum.Second, "Admin Menu." },
//        //    {StartMenuEnum.Third, "Info Menu." },
//        //    {StartMenuEnum.Exit, "Save & Exit." },
//        //};

//        //public void DisplayMenu()
//        //{
//        //    Console.Clear();
//        //    Console.WriteLine("Choose an option below.");
//        //    foreach (var item in _startMenu)
//        //    {
//        //        Console.WriteLine($"{(int)item.Key}. {item.Value}");
//        //    }
//        //}

//        //public void InitializeMenu()
//        //{
//        //    _fileManager.LoadDiscountListFromFile();
//        //    StartMenuEnum userInput;
//        //    do
//        //    {
//        //        DisplayMenu();
//        //        userInput = UserInputHandler.GetStartMenuEnum();
//        //        _startMenuHandler.MenuHandler(userInput);
//        //    } while (userInput != StartMenuEnum.Exit);
//        //}
//    }
//}