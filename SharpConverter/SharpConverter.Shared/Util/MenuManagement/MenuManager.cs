using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util.MenuManagement
{
    public class MenuManager
    {

        public string? Input { get; private set; }
        public string? Output { get; private set; }
        public bool IsConsole { get; private set; }
        public bool InNavigation { get; private set; }
        public MenuState MenuState { get; private set; }
        public string ErrorMessage { get; private set; }
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
                switch (MenuState)
                {
                    case MenuState.Default:
                        Console.WriteLine(MenuHeader());
                        Input = Console.ReadLine();
                        if (Input == null)
                            Input = "";
                        MenuState = DefaultMenuHandling(Input);
                        break;

                    case MenuState.Exit:
                        Console.WriteLine(ThankYouMessage());
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    case MenuState.Error:
                        Console.WriteLine(GetErrorMessage());
                        Console.ReadKey();
                        MenuState = MenuState.Default;
                        break;

                    case MenuState.Help:
                        break;

                    case MenuState.NSCMenu:
                        Console.WriteLine(NSCMenu());
                        Input = Console.ReadLine();
                        if (Input is null)
                            Input = "";
                        Console.WriteLine(NSCOperation(Input));
                        break;
                    case MenuState.IEEEMenu:
                        break;
                    case MenuState.UnicodeMenu:
                        break;
                    default:
                        break;
                }
            } while (InNavigation);
            
            
                
        }
        #region Assets
       
        private MenuState DefaultMenuHandling(string input)
        {
            MenuState menuState;
            switch (input)
            {
                case "1":
                    menuState = MenuState.NSCMenu;
                    break;
                case "2":
                    menuState = MenuState.IEEEMenu;
                    break;
                case "3":
                    menuState = MenuState.UnicodeMenu;
                    break;
                case "4":
                    menuState = MenuState.Exit;
                    break;
                default:
                    menuState = MenuState.Error;
                    break;
            }
            return menuState;
        }
        private string NSCOperation(string input)
        {
            
            string trimmedCommand = "";
            string from = "";
            string to = "";

            //TODO: Add arguments
            string arguments = "";
            bool isValid = false;

            //Checks if valid
            if (input.Contains("=>"))
                isValid = true;

            //Throw if invalid
            if (!isValid)
                MenuState = MenuState.Error;

            //If input has arguments...
            if(input.Contains('|'))
            {

                int index = input.IndexOf('|');
                for (int i = index; i < input.Length; i++)
                {
                    //Add to arguments string for later evalution
                    arguments += input[i];
                    
                }
                //Remove arguments from physical input
                input = input.Remove(index);
            }

            //Adds command by filtering
            foreach (var c in input)
            {
                if (!c.Equals(' ') || !c.Equals('\n') || !c.Equals('\t'))
                    trimmedCommand += c;
                
            }

            

            
            

            //return MenuState;
        }
        private static string NSCMenu()
        {
            string header = $"||=================#Converter-v0.1=================||" +
                           "\n|| SELECT A COVERSION TYPE:                        ||" +
                           "\n||=================================================||" +
                           "\n|| HOW IT WORKS:                                   ||" +
                           "\n|| Type the first letter of the system you'd like  ||" +
                           "\n|| to convert from, followed by '=>', and the      ||" +
                           "\n|| first letter of the system you'd like to        ||" +
                           "\n|| to convert to.                                  ||" +
                           "\n|| Add any arguments following a pipe '|'          ||" +
                           "\n|| For a list of available commands type 'help'.   ||" +
                           "\n||                                                 ||" +
                           "\n|| Example: For decimal to binary                  ||" +
                           "\n|| d => b                                          ||" +
                           "\n||                                                 ||" +
                           "\n|| Example: Octal to Hex, split by nibbles         ||" +
                           "\n|| o => h | -n                                     ||" +
                           "\n||                                                 ||" +
                           "\n||_________________________________________________||" +
                           "\n||=================================================||" +
                           "\n\nCOMMAND: ";

            return header;
        }
        private static string MenuHeader()
        {
            string header = $"||=================#Converter-v0.1=================||" +
                           "\n|| SELECT A COVERSION TYPE:                        ||" +
                           "\n||=================================================||" +
                           "\n|| 1. Number System Conversions                    ||" +
                           "\n|| 2. IEEE 754 Single Precision                    ||" +
                           "\n|| 3. UNICODE and ASCII Conversions                ||" +
                           "\n|| 4. Exit Program                                 ||" +
                           "\n||_________________________________________________||" +
                           "\n||=================================================||" +
                           "\n||============-lethargie_sleeps-2022-==============||" +
                           "\n\nSELECTION: ";

            return header;

        }
        private static string ThankYouMessage()
        {
            string message = "Thanks for using #Converter!\n" +
                $"Visit https://github.com/lethargiesleeps/SharpConverter if you would like to contribute!";
            return message;
        }
        private static string GetErrorMessage()
        {
            return "Invalid input. Type '-help' for help with a command";
        }

        
        #endregion

    }
}
