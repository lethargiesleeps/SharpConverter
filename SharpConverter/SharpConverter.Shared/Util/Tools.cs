using System.Text;
using SharpConverter.Shared.Util.MenuManagement;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util;

public static class Tools
{
    #region NumberHandling

    public static string SplitDecimal(bool postPoint, string input)
    {
        if (input.Contains("ERROR"))
            return input;

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

        if (input.ToLower().Equals("-exit"))
            return "mout";
        if (input.ToLower().Equals("-help"))
            return "maid";

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

        //Check for too many arguments
        if (hasArguments)
        {
            var tackCount = 0;
            for(var i = 0; i < input.Length; i++)
                if(input[i].Equals('-'))
                    tackCount++;

            if (tackCount > 1)
                return
                    "This converter currently only supports one argument at a time. A later version may allow for multiple arguments.";
        }

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
            for (var i = 0; i < cleanedCommand.Length; i++) command.Append(cleanedCommand[i]);
        }

        #endregion

        return getArguments ? arguments.ToString() : command.ToString();
    }

    public static StringBuilder ReverseString(StringBuilder preReverse)
    {
        var postReverse = new StringBuilder();

        for (var i = preReverse.Length - 1; i >= 0; i--) postReverse.Append(preReverse[i]);
        return postReverse;
    }

    public static string CleanInput(string input)
    {
        var cleanedInput = new StringBuilder();
        foreach (var c in input.Where(c => !c.Equals(' '))) cleanedInput.Append(c);
        return cleanedInput.ToString();
    }

    #endregion

    #region StateProcessing

    public static CommandState ParseCommand(string command)
    {
        const int maxCommandLength = 4;
        if (command.Length > maxCommandLength)
            return CommandState.Error;

        //Gets from at index 0, assuming command is valid
        var from = command[0];

        //Gets to at index 3, assuming command is valid
        var to = command[3];

        if (command.ToLower().Equals("maid"))
            return CommandState.Help;

        if (command.ToLower().Equals("mout"))
            return CommandState.Exit;

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

    public static string ParseArguments(string arguments, string nscOutput, ArgumentHandler argumentHandler, CommandState command, string originalValue)
    {
        StringBuilder returnString = new();

        // 0 = convert all -a
        // 1 = ones complement -o
        // 2 = twos complements -l
        // 3 = split by nibble -n
        // 4 = split by byte -b
        // 5 = split by word -w
        // 6 = split ternary -t
        // 7 = prefix zeros -p

        var argumentValidations = new bool[argumentHandler.MaxArguments];

        //Gets arguments
        if (string.IsNullOrEmpty(arguments))
            return nscOutput;
        if (string.IsNullOrWhiteSpace(arguments))
            return nscOutput;
        if (nscOutput.Contains("ERROR"))
            return nscOutput;
        if (arguments.Contains("-a"))
            argumentValidations[0] = true;
        if (arguments.Contains("-o"))
            argumentValidations[1] = true;
        if (arguments.Contains("-l"))
            argumentValidations[2] = true;
        if (arguments.Contains("-n"))
            argumentValidations[3] = true;
        if (arguments.Contains("-b"))
            argumentValidations[4] = true;
        if (arguments.Contains("-w"))
            argumentValidations[5] = true;
        if (arguments.Contains("-t"))
            argumentValidations[6] = true;
        if (arguments.Contains("-p"))
            argumentValidations[7] = true;
        //Add extra arguments here if they arise

        //Illegal argument combos
        if (argumentValidations[0])
            returnString.Append(argumentHandler.ConvertAll(command, originalValue));
        if (argumentValidations[1])
            returnString.Append(argumentHandler.GetOnesComplement(argumentHandler.ArgumentState, nscOutput));
        if (argumentValidations[2])
            returnString.Append(argumentHandler.GetTwosComplement(argumentHandler.ArgumentState, nscOutput));

        return returnString.ToString();
    }

    #endregion
}