using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpConverter.Shared.Util.Converters;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;
 


namespace SharpConverter.Shared.Util.MenuManagement
{
    public class MenuManager
    {
        public string Input { get; private set; }
        public bool IsConsole { get; private set; }
        public bool InNavigation { get; private set; }
        public MenuState MenuState { get; private set; }
        public MenuState LastMenuState { get; private set; }
        public ErrorState ErrorState { get; private set; }
        public NumberSystemConverter NumberSystemConverter { get; private set; }

        public MenuManager(NumberSystemConverter numberSystemConverter)
        {
            this.NumberSystemConverter = numberSystemConverter;
            this.InNavigation = true;
            MenuState = MenuState.Default;
        }

        public MenuManager(bool isConsole, NumberSystemConverter numberSystemConverter):this(numberSystemConverter)
        {
            this.IsConsole = isConsole;
        }

        public void MainMenu(bool isConsole)
        {
            if (isConsole)
            {
                ConsoleMenuLoop();
            }
            else
            {
                throw new NotImplementedException();
            }
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
                {
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
                }
                //Number Systems Conversion
                while (MenuState == MenuState.NSCMenu)
                {
                    string command = "";
                    string arguments = "";
                    string conversion = "";
                    Console.Clear();
                    Console.WriteLine(ConsoleMenuTools.NSCMenu());
                    Input = Console.ReadLine();
                    if (Input == null)
                        throw new NullReferenceException("Hmmm, was null in MenuManager @ Line 136");

                    command = Tools.SplitConversionCommand(Input);
                    arguments = Tools.SplitConversionCommand(Input,true);
                    CommandState commandState = Tools.ParseCommand(command);
                    string getNumber = "";

                    switch (commandState)
                    {
                        case CommandState.DecimalToBinary:
                            Console.WriteLine("\nEnter a decimal number: ");
                            getNumber = Console.ReadLine();
                            Console.WriteLine("\n" + NumberSystemConverter.DecimalToBinary(getNumber));
                            Console.WriteLine("\nPress any key to continue...");
                            break;
                        case CommandState.DecimalToOctal:
                            Console.WriteLine("\nEnter a decimal number: ");
                            getNumber = Console.ReadLine();
                            Console.WriteLine("\n" + NumberSystemConverter.DecimalToOctal(getNumber));
                            Console.WriteLine("\nPress any key to continue...");
                            break;
                        case CommandState.DecimalToHexadecimal:
                            break;
                        case CommandState.BinaryToDecimal:
                            break;
                        case CommandState.BinaryToOctal:
                            break;
                        case CommandState.BinaryToHexadecimal:
                            break;
                        case CommandState.OctalToDecimal:
                            break;
                        case CommandState.OctalToBinary:
                            break;
                        case CommandState.OctalToHexadecimal:
                            break;
                        case CommandState.HexadecimalToDecimal:
                            break;
                        case CommandState.HexadecimalToBinary:
                            break;
                        case CommandState.HexadecimalToOctal:
                            break;
                        case CommandState.DecimalToDecimal:
                            Console.WriteLine("Enter a decimal number: ");
                            getNumber = Console.ReadLine();
                            Console.WriteLine("\n" + NumberSystemConverter.ReturnOriginalValue(getNumber));
                            break;
                        case CommandState.BinaryToBinary:
                            break;
                        case CommandState.OctalToOctal:
                            break;
                        case CommandState.HexadecimalToHexadecimal:
                            break;
                        case CommandState.Error:
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
}
