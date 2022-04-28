using System.Text;
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
        ArgumentHandler = new ArgumentHandler(ArgumentState.Default, NumberSystemConverter);
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
    public ArgumentHandler ArgumentHandler { get; }
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
                Console.Clear();
                Console.WriteLine(ConsoleMenuTools.NSCMenu());
                ArgumentHandler.SetArgumentState(ArgumentState.Default);
                Input = Console.ReadLine();
                if (Input == null)
                    throw new NullReferenceException("Hmmm, was null in MenuManager @ Line 136");

                command = Tools.SplitConversionCommand(Input);
                //TODO: Handle arguments
                arguments = Tools.SplitConversionCommand(Input, true);
                var commandState = Tools.ParseCommand(command);

                //TODO: Remove, for debugging
                Console.WriteLine($"Arguments: {arguments}");
                Console.WriteLine($"Command State: {commandState}");
                Console.WriteLine($"Command: {command}");
                Console.WriteLine();

                var getNumber = string.Empty;
                var nscOutput = new StringBuilder();
                switch (commandState)
                {
                    case CommandState.DecimalToBinary:
                        Console.WriteLine("Enter a DECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.DecimalToBinary(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Binary);
                        break;
                    case CommandState.DecimalToOctal:
                        Console.WriteLine("Enter a DECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.DecimalToOctal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Octal);
                        break;
                    case CommandState.DecimalToHexadecimal:
                        Console.WriteLine("Enter a DECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.DecimalToHexadecimal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Hexadecimal);
                        break;
                    case CommandState.BinaryToDecimal:
                        Console.WriteLine("Enter a BINARY number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.BinaryToDecimal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Decimal);
                        break;
                    case CommandState.BinaryToOctal:
                        Console.WriteLine("Enter a BINARY number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.BinaryToOctal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Octal);
                        break;
                    case CommandState.BinaryToHexadecimal:
                        Console.WriteLine("Enter a BINARY number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.BinaryToHexadecimal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Hexadecimal);
                        break;
                    case CommandState.OctalToDecimal:
                        Console.WriteLine("Enter an OCTAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.OctalToDecimal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Decimal);
                        break;
                    case CommandState.OctalToBinary:
                        Console.WriteLine("Enter an OCTAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.OctalToBinary(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Binary);
                        break;
                    case CommandState.OctalToHexadecimal:
                        Console.WriteLine("Enter an OCTAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.OctalToHexadecimal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Hexadecimal);
                        break;
                    case CommandState.HexadecimalToDecimal:
                        Console.WriteLine("Enter a HEXADECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.HexadecimalToDecimal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Decimal);
                        break;
                    case CommandState.HexadecimalToBinary:
                        Console.WriteLine("Enter a HEXADECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.HexadecimalToBinary(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Binary);
                        break;
                    case CommandState.HexadecimalToOctal:
                        Console.WriteLine("Enter a HEXADECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.HexadecimalToOctal(getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Octal);
                        break;
                    case CommandState.DecimalToDecimal:
                        Console.WriteLine("Enter a DECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.ReturnOriginalValue(commandState, getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Decimal);
                        break;
                    case CommandState.BinaryToBinary:
                        Console.WriteLine("Enter a BINARY number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.ReturnOriginalValue(commandState, getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Binary);
                        break;
                    case CommandState.OctalToOctal:
                        Console.WriteLine("Enter an OCTAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.ReturnOriginalValue(commandState, getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Octal);
                        break;
                    case CommandState.HexadecimalToHexadecimal:
                        Console.WriteLine("Enter a HEXADECIMAL number:");
                        getNumber = Console.ReadLine();
                        nscOutput.Append(NumberSystemConverter.ReturnOriginalValue(commandState, getNumber!));
                        ArgumentHandler.SetArgumentState(ArgumentState.Hexadecimal);
                        break;
                    case CommandState.Error:
                        Console.WriteLine($"{command}");
                        Console.WriteLine(
                            "Command was invalid. Type '-help' at any time for more info, or -exit to go back to main menu.");
                        Console.WriteLine("Press any key to continue...");
                        ArgumentHandler.SetArgumentState(ArgumentState.Error);
                        break;
                    case CommandState.Help:
                        //TODO: Implement when arguments are sorted out
                        break;
                    case CommandState.Exit:
                        MenuState = MenuState.Default;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Console.WriteLine(Tools.ParseArguments(arguments, nscOutput.ToString(), ArgumentHandler, commandState, getNumber!));

                Console.ReadKey();
            }
        } while (InNavigation);

        Environment.Exit(0);
    }
}