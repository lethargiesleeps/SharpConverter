using System.Text;
using SharpConverter.Shared.Util.Contracts;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Converters;

public class NumberSystemConverter : INumberSystemConverter, INSCInputValidator
{
    #region Conversions

    public string DecimalToBinary(string decimalValue)
    {
        //Declarations
        double conversionTest = 0;
        var finalConversion = new StringBuilder();
        var beforeFloatingPoint = 0;
        var afterFloatingPoint = 0;

        var input = ValidateDecimalInput(decimalValue);
        var isValid = false;

        //Validation
        try
        {
            conversionTest = double.Parse(input);
        }
        catch (Exception e)
        {
            isValid = false;
        }
        finally
        {
            isValid = true;
        }

        if (isValid)
        {
            //TODO:Handle floating points
            if (input.Contains('.'))
            {
                beforeFloatingPoint = int.Parse(Tools.SplitDecimal(false, input));
                afterFloatingPoint = int.Parse(Tools.SplitDecimal(true, input));
            }
            else
            {
                beforeFloatingPoint = int.Parse(input);
                var binaryArray = new int[byte.MaxValue];
                for (var i = 0; beforeFloatingPoint > 0; i++)
                {
                    binaryArray[i] = beforeFloatingPoint % 2;
                    beforeFloatingPoint /= 2;
                    finalConversion.Append(binaryArray[i]);
                }

                finalConversion = Tools.ReverseString(finalConversion);
            }

            return finalConversion.ToString();
        }

        return input; //Returns error message
    }

    public string BinaryToDecimal(string binaryValue)
    {
        throw new NotImplementedException();
    }

    public string DecimalToHexadecimal(string decimalValue)
    {
        //Declarations
        double conversionTest = 0;
        var finalConversion = new StringBuilder();
        var beforeFloatingPoint = 0;
        var afterFloatingPoint = 0;
        var input = ValidateDecimalInput(decimalValue);
        var isValid = false;

        //Validation
        try
        {
            conversionTest = double.Parse(input);
        }
        catch (Exception e)
        {
            isValid = false;
        }
        finally
        {
            isValid = true;
        }

        //Conversion logic
        if (isValid)
        {
            //TODO:Handle floating points
            if (input.Contains('.'))
            {
                beforeFloatingPoint = int.Parse(Tools.SplitDecimal(false, input));
                afterFloatingPoint = int.Parse(Tools.SplitDecimal(true, input));
            }
            else
            {
                beforeFloatingPoint = int.Parse(input);
                var hexArray = new int[byte.MaxValue];
                for (var i = 0; beforeFloatingPoint > 0; i++)
                {
                    hexArray[i] = beforeFloatingPoint % 16;
                    beforeFloatingPoint /= 16;
                    finalConversion.Append(GetHexValues()[hexArray[i]]);
                }

                finalConversion = Tools.ReverseString(finalConversion);
            }

            return finalConversion.ToString();
        }

        return input; //Returns error message
    }

    public string HexadecimalToDecimal(string hexValue)
    {
        throw new NotImplementedException();
    }

    public string DecimalToOctal(string decimalValue)
    {
        double conversionTest = 0;
        var finalConversion = new StringBuilder();
        var beforeFloatingPoint = 0;
        var afterFloatingPoint = 0;
        var input = ValidateDecimalInput(decimalValue);
        var isValid = false;

        //Validation
        try
        {
            conversionTest = double.Parse(input);
        }
        catch (Exception e)
        {
            isValid = false;
        }
        finally
        {
            isValid = true;
        }

        if (isValid)
        {
            //TODO:Handle floating points
            if (input.Contains('.'))
            {
                beforeFloatingPoint = int.Parse(Tools.SplitDecimal(false, input));
                afterFloatingPoint = int.Parse(Tools.SplitDecimal(true, input));
            }
            else
            {
                beforeFloatingPoint = int.Parse(input);
                var octalArray = new int[byte.MaxValue];
                for (var i = 0; beforeFloatingPoint > 0; i++)
                {
                    octalArray[i] = beforeFloatingPoint % 8;
                    beforeFloatingPoint /= 8;
                    finalConversion.Append(octalArray[i]);
                }

                finalConversion = Tools.ReverseString(finalConversion);
            }

            return finalConversion.ToString();
        }

        return input; //Returns error message;
    }

    public string OctalToDecimal(string octalValue)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Assets

    private char[] GetHexValues()
    {
        var hexValues = new char[16]
        {
            '0', '1', '2',
            '3', '4', '5',
            '6', '7', '8',
            '9', 'A', 'B',
            'C', 'D', 'E', 'F'
        };


        return hexValues;
    }

    public bool[] ValidationChecks()
    {
        //0 = isNullOrEmptyOrWhitespace
        //1 = hasTooManyFloatingPoints
        //2 = isNegativeValue
        //3 = containsLetters
        //4 = illegalCharactersInNumberSystem
        var checks = new bool[5]
        {
            false, false, false,
            false, false
        };

        return checks;
    }

    public string ReturnOriginalValue(string input)
    {
        throw new NotImplementedException();
    }

    public string MultiConverted(CommandState command)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Validation

    public string ValidateDecimalInput(string input)
    {
        var errorInput = new StringBuilder();
        var cleanedInput = new StringBuilder();
        var numberOfFloatingPoints = 0;
        var hasFloatingPoint = false;
        var isValid = false;
        var checks = ValidationChecks();


        //Error message instantiation
        errorInput.Append("ERROR:\n");
        //Check null, empty, whitespace
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            errorInput.Append("Input cannot be empty or whitespace.\n");
            checks[0] = true;
        }


        //Floating point check
        if (input.Contains('.'))
            hasFloatingPoint = true;
        if (hasFloatingPoint)
            foreach (var i in input)
                if (i.Equals('.'))
                    numberOfFloatingPoints++;

        if (numberOfFloatingPoints > 1)
        {
            errorInput.Append("Input has too many floating points. Can only contain 1 floating point\n");
            checks[1] = true;
        }


        //Negative number check
        if (input.Length > 0)
        {
            if (input[0].Equals('-'))
            {
                errorInput.Append(
                    "This converter cannot handle negative values with adequate precision. Use the IEEE converter if handling negative decimal values.\n");
                checks[2] = true;
            }
        }

        //Check is there are letters or illegal symbols
        foreach (var c in input)
            if (!char.IsDigit(c) && !c.Equals('.') && !c.Equals(' '))
                checks[3] = true;

        if (checks[3])
            errorInput.Append("Decimal numbers cannot contain letters or illegal symbols.\n");

        if (!checks[0] && !checks[1] && !checks[2] && !checks[3])
            isValid = true;

        if (isValid)
            foreach (var c in input.Where(c => char.IsDigit(c) || c.Equals('.')))
                cleanedInput.Append(c);


        errorInput.Append("Press any key to try again...");
        //Final result
        return isValid ? cleanedInput.ToString() : errorInput.ToString();
    }

    public string ValidateOctalInput(string input)
    {
        throw new NotImplementedException();
    }

    public string ValidateHexadecimalInput(string input)
    {
        throw new NotImplementedException();
    }

    public string ValidateBinaryInput(string input)
    {
        //TODO: Can I refactor this using a character array of acceptable characters instead of those long af conditions
        var errorInput = new StringBuilder();
        var cleanedInput = new StringBuilder();
        var numberOfFloatingPoints = 0;
        var hasFloatingPoint = false;
        var isValid = false;
        var checks = ValidationChecks();

        //Error message instantiation
        errorInput.Append("ERROR:\n");
        //Check null, empty, whitespace
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            errorInput.Append("Input cannot be empty or whitespace.\n");
            checks[0] = true;
        }

        //Floating point check
        if (input.Contains('.'))
            hasFloatingPoint = true;
        if(hasFloatingPoint)
            foreach(var i in input)
                if (i.Equals('.'))
                    numberOfFloatingPoints++;

        if (numberOfFloatingPoints > 1)
        {
            errorInput.Append("Input has too many floating points. Can only contain 1 floating point\n");
            checks[1] = true;
        }

        //Negative number check
        if (input.Length > 0)
        {
            if (input[0].Equals('-'))
            {
                errorInput.Append(
                    "This converter cannot handle binary values with '-'. Use the IEEE converter if handling negative binary values.\n");
                checks[2] = true;
            }
        }
        
        //Check is there are letters or illegal symbols
        foreach (var c in input)
            if (!char.IsDigit(c) && !c.Equals('.') && !c.Equals(' '))
                checks[3] = true;

        if (checks[3])
            errorInput.Append("Decimal numbers cannot contain letters or illegal symbols.\n");

        //Check if not a 0 or 1
        foreach(var c in input)
            if(!c.Equals('0') && !c.Equals('1') && !c.Equals(' ') && !c.Equals('.'))
                checks[4] = true;

        if (checks[4])
            errorInput.Append("Binary values can only consist of '0's and '1's.\n");

        if (!checks[0] && !checks[1] && !checks[2] && !checks[3] && !checks[4])
            isValid = true;

        if (isValid)
            foreach (var c in input.Where(c => char.IsDigit(c) || c.Equals('.')))
                cleanedInput.Append(c);

        errorInput.Append("Press any key to try again...");
        //Final result
        return isValid ? cleanedInput.ToString() : errorInput.ToString();




    }

    #endregion
}