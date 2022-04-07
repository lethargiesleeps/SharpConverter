using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util
{
    public class MenuManager
    {

        public string? Input { get; private set; }
        public string? Output { get; private set; }
        public bool IsConsole { get; private set; }
        public bool[] MenuSwitches { get; private set; }
        public NumberSystemConverter NumberSystemConverter { get; private set; }

        public MenuManager(NumberSystemConverter numberSystemConverter)
        {
            this.NumberSystemConverter = numberSystemConverter;
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
            
                
        }
        #region Assets
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

        private void ResetSwitches(bool[] switches, bool includeDefault)
        {
            if (!includeDefault)
            {
                for (int i = 1; i < switches.Length; i++)
                    switches[i] = false;
            }
            else
            {
                for (int i = 0; i < switches.Length; i++)
                    switches[i] = false;
            }
        }
        #endregion

    }
}
