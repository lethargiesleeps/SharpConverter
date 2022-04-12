using System.Text;

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
            if (characters[i] == '.')
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

    

    #endregion
}