using System.Threading.Channels;
using SharpConverter.Shared.Util.Converters;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.MenuManagement;

public class MenuManager
{
    public MenuManager(NumberSystemConverter numberSystemConverter)
    {
        NumberSystemConverter = numberSystemConverter;
        InNavigation = true;
        MenuState = MenuState.Default;
    }

    public MenuManager(bool isConsole, NumberSystemConverter numberSystemConverter) : this(numberSystemConverter)
    {
        IsConsole = isConsole;
    }

    public string? Input { get; private set; }
    public bool IsConsole { get; }
    public bool InNavigation { get; private set; }
    public MenuState MenuState { get; private set; }
    public MenuState LastMenuState { get; private set; }
    public ErrorState ErrorState { get; private set; }
    public NumberSystemConverter NumberSystemConverter { get; }

    public void MainMenu(bool isConsole)
    {
        if (isConsole)
            ConsoleMenuLoop();
        else
            throw new NotImplementedException();
    }


    public void ConsoleMenuLoop()
    {
        do
        {
            Console.Clear();
            //Main Menu
            while (MenuState == MenuState.Default)
            {
                Console.WriteLine(ConsoleMenuTools.MenuHeader());
                Input = Console.ReadLine();
                switch (Input)
                {
                    case "1":
                        MenuState = MenuState.NSCMenu;
                        LastMenuState = MenuState.Default;
                        break;
                    case "2": //TODO: IEEE
                        MenuState = MenuState.NotDefined;
                        LastMenuState = MenuState.Default;
                        break;
                    case "3": //TODO: Unicode Conversion
                        MenuState = MenuState.NotDefined;
                        LastMenuState = MenuState.Default;
                        break;
                    case "4":
                        MenuState = MenuState.Exit;
                        LastMenuState = MenuState.Default;
                        break;
                    default:
                        MenuState = MenuState.Error;
                        ErrorState = ErrorState.Default;
                        LastMenuState = MenuState.Default;
                        break;
                }
            }

            //Exit program
            while (MenuState == MenuState.Exit)
            {
                Console.Clear();
                Console.WriteLine(ConsoleMenuTools.ThankYouMessage());
                Console.ReadKey();
                InNavigation = false;
                break;
            }

            //Error menu TODO:Error menu needs work
            while (MenuState == MenuState.Error)
                switch (ErrorState)
                {
                    case ErrorState.Default:
                        Console.WriteLine(ConsoleMenuTools.GetErrorMessage(ErrorState));
                        Console.ReadKey();
                        ErrorState = ErrorState.None;
                        MenuState = LastMenuState;
                        break;
                    case ErrorState.NSC:
                        Console.WriteLine(ConsoleMenuTools.GetErrorMessage(ErrorState));
                        Console.ReadKey();
                        ErrorState = ErrorState.None;
                        MenuState = LastMenuState;
                        break;
                    case ErrorState.IEEE:
                        Console.WriteLine(ConsoleMenuTools.GetErrorMessage(ErrorState));
                        Console.ReadKey();
                        ErrorState = ErrorState.None;
                        MenuState = LastMenuState;
                        break;
                    case ErrorState.Unicode:
                        Console.WriteLine(ConsoleMenuTools.GetErrorMessage(ErrorState));
                        Console.ReadKey();
                        ErrorState = ErrorState.None;
                        MenuState = LastMenuState;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            //Number Systems Conversion
            while (MenuState == MenuState.NSCMenu)
            {
                var command = "";
                var arguments = "";
                var conversion = "";
                Console.Clear();
                Console.WriteLine(ConsoleMenuTools.NSCMenu());
                Input = Console.ReadLine();
                if (Input == null)
                    throw new NullReferenceException("Hmmm, was null in MenuManager @ Line 136");

                command = Tools.SplitConversionCommand(Input);
                //TODO: Handle arguments
                arguments = Tools.SplitConversionCommand(Input, true);
                var commandState = Tools.ParseCommand(command);
                var getNumber = "";

                switch (commandState)
                {
                    case CommandState.DecimalToBinary:
                        Console.WriteLine("This will convert decimal to binary");
                        Console.ReadKey();
                        break;
                    case CommandState.DecimalToOctal:
                        Console.WriteLine("This will convert decimal to octal");
                        Console.ReadKey();
                        break;
                    case CommandState.DecimalToHexadecimal:
                        Console.WriteLine("This will convert decimal to hex");
                        Console.ReadKey();
                        break;
                    case CommandState.BinaryToDecimal:
                        Console.WriteLine("This will convert binary to decimal");
                        Console.ReadKey();
                        break;
                    case CommandState.BinaryToOctal:
                        Console.WriteLine("This will convert binary to octal");
                        Console.ReadKey();
                        break;
                    case CommandState.BinaryToHexadecimal:
                        Console.WriteLine("This will convert binary to hex");
                        Console.ReadKey();
                        break;
                    case CommandState.OctalToDecimal:
                        Console.WriteLine("This will convert octal to decimal");
                        Console.ReadKey();
                        break;
                    case CommandState.OctalToBinary:
                        Console.WriteLine("This will convert octal to binary");
                        Console.ReadKey();
                        break;
                    case CommandState.OctalToHexadecimal:
                        Console.WriteLine("This will convert octal to hex");
                        Console.ReadKey();
                        break;
                    case CommandState.HexadecimalToDecimal:
                        Console.WriteLine("This will convert hex to decimal");
                        Console.ReadKey();
                        break;
                    case CommandState.HexadecimalToBinary:
                        Console.WriteLine("This will convert hex to binary");
                        Console.ReadKey();
                        break;
                    case CommandState.HexadecimalToOctal:
                        Console.WriteLine("This will hex to octal");
                        Console.ReadKey();
                        break;
                    case CommandState.DecimalToDecimal:
                        Console.WriteLine("This will return original.");
                        Console.ReadKey();
                        break;
                    case CommandState.BinaryToBinary:
                        Console.WriteLine("This will return original.");
                        Console.ReadKey();
                        break;
                    case CommandState.OctalToOctal:
                        Console.WriteLine("This will return original.");
                        Console.ReadKey();
                        break;
                    case CommandState.HexadecimalToHexadecimal:
                        Console.WriteLine("This will return original.");
                        Console.ReadKey();
                        break;
                    case CommandState.Error:
                        //TODO: Specific error handling
                        Console.WriteLine("Command was invalid. Type '-help' at any time for more info.");
                        Console.WriteLine("Press any key to continue...");
                        break;
                    case CommandState.Help:
                        //TODO: Implement when arguments are sorted out
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Console.ReadKey();
            }
        } while (InNavigation);

        Environment.Exit(0);
    }
}