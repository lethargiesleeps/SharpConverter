using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util.MenuManagement
{
    public class MenuManager
    {
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

            } while (InNavigation);
        }
        #region Assets
       
        //To complicated
        public string[] NSCOperation(string input)
        {
            #region StringManipulation
            string trimmedCommand = "";
            string[] fromXToY = new string[2];

            //TODO: Add arguments
            string arguments = "";
            bool isValid = false;

            //Checks if valid based on proper usage of =>, and minimum needed characters in input (4)
            if (input.Contains("=>") || input.Length > 4)
                isValid = true;

            //Throw if invalid
            if (!isValid)
            {
                fromXToY[0] = "Nope";
                fromXToY[1] = "Sorry";
                MenuState = MenuState.Error;
                return fromXToY;
                
            }
                

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

            //Get command parameters
            fromXToY = trimmedCommand.Split(new char[2] { '=', '>'}, StringSplitOptions.TrimEntries);

            //Change whitespace to avoid confusion in loops
            
            #endregion
            if (fromXToY is null)
                throw new NullReferenceException("Hmmm something went wrong... In MenuManager @ Line 173");

            return fromXToY;

        }
        

        
        #endregion

    }
}
