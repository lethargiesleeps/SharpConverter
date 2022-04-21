using System.Text;
using SharpConverter.Shared.Util.Converters;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util;

public static class Tools
{
    #region NumberHandling

    public static string SplitDecimal(bool postPoint, string input)
    {
        var characters = input.ToCharArray();
        var pointIndex = 0;
        var returnValue = "";

        for (var i = 0; i < characters.Length; i++)
            if (characters[i].Equals('.'))
                pointIndex = i;

        if (!postPoint)
        {
            var returnValueArray = new char[pointIndex];

            for (var i = 0; i < pointIndex; i++)
                returnValueArray[i] = characters[i];
            foreach (var c in returnValueArray) returnValue += c;
        }
        else
        {
            var returnValueArray = new char[characters.Length];
            for (var i = pointIndex + 1; i < characters.Length; i++)
                returnValueArray[i] = characters[i];
            foreach (var c in returnValueArray) returnValue += c;
        }

        return returnValue;
    }

   

    #endregion
    #region StringHandling

    public static string SplitConversionCommand(string input, bool getArguments = false)
    {
        const int minInputLength = 4;
        var acceptableChars = new char[4] {'=', '>', '|', '-'};
        var cleanedCommand = new StringBuilder();
        var command = new StringBuilder();
        var arguments = new StringBuilder();
        var commandParameters = new string[2] {"", ""};
        var hasArguments = false;

        #region ValidationChecks

        if (input is null)
            return "Input was null. Cannot proceed.";

        if (!input.Contains("=>"))
            return "Invalid command structure. Are you using => ?";

        if (input.Length < minInputLength)
            return "Invalid command structure. Command is to short.";

        //Check for any numbers
        foreach (var c in input)
            if (char.IsDigit(c))
                return "Invalid command structure. No need for numbers in a command.";

        //Check if input has suffixed arguments
        //TODO: Argument handling
        if (input.Contains('|'))
            hasArguments = true;

        //Check for unnecessary symbols
        var containsBadSymbols = false;
        for (var i = 0; i < input.Length; i++)
        {
            if (!char.IsSymbol(input[i])) continue;
            if (input[i] != acceptableChars[0] &&
                input[i] != acceptableChars[1] &&
                input[i] != acceptableChars[2] &&
                input[i] != acceptableChars[3])
                containsBadSymbols = true;
        }

        if (containsBadSymbols)
            return "Invalid command structure: Invalid symbols used.";

        //Check if to many pipes
        var pipeCount = 0;
        for (var i = 0; i < input.Length; i++)
            if (input[i] == '|')
                pipeCount++;

        if (pipeCount > 1)
            return "Invalid command structure. Only one '|' is accepted";

        //Check if too many = and >
        var equalsCount = 0;
        var arrowCount = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == '=')
                equalsCount++;
            if (input[i] == '>')
                arrowCount++;
        }

        if (equalsCount > 1 || arrowCount > 1)
            return "Invalid command structure: Too many '=' or '>' used in command.";

        #endregion

        #region InputCleaningLogicAndSplitting

        //Remove any whitespace characters
        foreach (var c in input)
            if (!char.IsWhiteSpace(c))
                cleanedCommand.Append(c);

        //Get arguments
        if (hasArguments)
        {
            var index = cleanedCommand.ToString().IndexOf('|');
            var cleanerReverse = 0;
            //Store in command sb
            for (var i = index - 1; i >= 0; i--)
            {
                command.Append(cleanedCommand[cleanerReverse]);
                cleanerReverse++;
            }

            //Store in arguments sb
            for (var i = index + 1; i < cleanedCommand.Length; i++)
                if (!char.IsWhiteSpace(cleanedCommand[i]))
                    arguments.Append(cleanedCommand[i]);
        }
        else
        {

            for (var i = 0; i < cleanedCommand.Length; i++)
            {
                command.Append(cleanedCommand[i]);
                
            }
        }

        #endregion

        return getArguments ? arguments.ToString() : command.ToString();
    }

    public static StringBuilder ReverseString(StringBuilder preReverse)
    {
        StringBuilder postReverse = new StringBuilder();

        for (int i = preReverse.Length-1; i >= 0; i--)
        {
            postReverse.Append(preReverse[i]);
        }
        return postReverse;

    }

    



    #endregion
    #region StateProcessing

    public static CommandState ParseCommand(string command)
    {
        const int maxCommandLength = 4;
        if (command.Length > maxCommandLength)
            return CommandState.Error;

        //Gets from at index 0, assuming command is valid
        char from = command[0];

        //Gets to at index 3, assuming command is valid
        char to = command[3];

        if (command.ToLower().Equals("-help".ToLower()))
            return CommandState.Help;

        switch (from)
        {
            case 'd':
                switch (to)
                {
                    case 'd':
                        return CommandState.DecimalToDecimal;
                    case 'b':
                        return CommandState.DecimalToBinary;
                    case 'o':
                        return CommandState.DecimalToOctal;
                    case 'h':
                        return CommandState.DecimalToHexadecimal;
                    default:
                        return CommandState.Error;
                        
                }
            case 'b':
                switch (to)
                {
                    case 'd':
                        return CommandState.BinaryToDecimal;
                    case 'b':
                        return CommandState.BinaryToBinary;
                    case 'o':
                        return CommandState.BinaryToOctal;
                    case 'h':
                        return CommandState.BinaryToHexadecimal;
                    default:
                        return CommandState.Error;
                }
            case 'h':
                switch (to)
                {
                    case 'd':
                        return CommandState.HexadecimalToDecimal;
                    case 'b':
                        return CommandState.HexadecimalToBinary;
                    case 'o':
                        return CommandState.HexadecimalToOctal;
                    case 'h':
                        return CommandState.HexadecimalToHexadecimal;
                    default:
                        return CommandState.Error;
                }
            case 'o':
                switch (to)
                {
                    case 'd':
                        return CommandState.OctalToDecimal;
                    case 'b':
                        return CommandState.OctalToBinary;
                    case 'o':
                        return CommandState.OctalToOctal;
                    case 'h':
                        return CommandState.OctalToHexadecimal;
                    default:
                        return CommandState.Error;
                }
            default:
                return CommandState.Error;
        }
    }


    #endregion

}