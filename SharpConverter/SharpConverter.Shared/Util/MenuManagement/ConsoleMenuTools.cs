using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.MenuManagement;

public static class ConsoleMenuTools
{
    public static string NSCMenu()
    {
        var header = "||=================#Converter-v0.4=================||" +
                     "\n|| SELECT A COVERSION TYPE:                        ||" +
                     "\n||=================================================||" +
                     "\n|| HOW IT WORKS:                                   ||" +
                     "\n|| Type the first letter of the system you'd like  ||" +
                     "\n|| to convert from, followed by '=>', and the      ||" +
                     "\n|| first letter of the system you'd like to        ||" +
                     "\n|| to convert to.                                  ||" +
                     "\n|| Add any arguments following a pipe '|'          ||" +
                     "\n|| For a list of available commands type 'help'.   ||" +
                     "\n|| To go back to the main menu, type 'back'        ||" +
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

    public static string MenuHeader()
    {
        var header = "||=================#Converter-v0.4=================||" +
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

    public static string ThankYouMessage()
    {
        var message = "Thanks for using #Converter!\n" +
                      "Visit https://github.com/lethargiesleeps/SharpConverter if you would like to contribute!";
        return message;
    }

    public static string GetErrorMessage(ErrorState errorState)
    {
        //TODO: Generate unique error messages
        return "Invalid input. Type '-help' for help with a command";
    }

    public static string GetHelpMessage(HelpState helpState)
    {
        //TODO: Implement unique help messages
        return "Not yet implemented";
    }
}