using System.Diagnostics.CodeAnalysis;
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
        StringBuilder finalConversion = new StringBuilder();
        int beforeFloatingPoint = 0;
        int afterFloatingPoint = 0;
        ;
        string input = ValidateDecimalInput(decimalValue);
        bool isValid = false;

        //Validation
        try
        {
            conversionTest = double.Parse(input);
            isValid = true;
        }
        catch (Exception e)
        {
            isValid = false;
        }

        if (isValid)
        {
            //Handle floating points
            if (input.Contains('.'))
            {
                beforeFloatingPoint = int.Parse(Tools.SplitDecimal(false,input));
                afterFloatingPoint = int.Parse(Tools.SplitDecimal(true, input));
            }
            else
            {
                beforeFloatingPoint = int.Parse(input);
                int[] binaryArray = new int[byte.MaxValue];
                for (int i = 0; beforeFloatingPoint > 0; i++)
                {
                    binaryArray[i] = beforeFloatingPoint % 2;
                    beforeFloatingPoint /= 2;
                    finalConversion.Append(binaryArray[i]);


                }

                finalConversion = Tools.ReverseString(finalConversion);



            }

            return finalConversion.ToString();

        }
        else
            return input; //Returns error message


    }

    public string BinaryToDecimal(string binaryValue)
    {
        throw new NotImplementedException();
    }

    public string DecimalToHexadecimal(string decimalValue)
    {
        throw new NotImplementedException();
    }

    public string HexadecimalToDecimal(string hexValue)
    {
        throw new NotImplementedException();
    }

    public string DecimalToOctal(string decimalValue)
    {
        throw new NotImplementedException();
    }

    public string OctalToDecimal(string octalValue)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Assets

    public char[] GetHexValues()
    {
        char[] hexValues = new char[16]
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
        //4 = overMaxSize
        bool[] checks = new bool[4]
        {
            false, false, false,
            false

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

    //TODO:Validate Decimal Input
    public string ValidateDecimalInput(string input)
    {
        StringBuilder errorInput = new StringBuilder();
        StringBuilder cleanedInput = new StringBuilder();
        int numberOfFloatingPoints = 0;
        bool hasFloatingPoint = false;
        bool isValid = false;
        bool[] checks = ValidationChecks();
        

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
        {
            foreach (var i in input)
            {
                if (i.Equals('.'))
                    numberOfFloatingPoints++;
            }
        }

        if (numberOfFloatingPoints > 1)
        {
            errorInput.Append("Input has too many floating points. Can only contain 1 floating point\n");
            checks[1] = true;
        }
            

        //Negative number check
        if (input[0].Equals('-'))
        {
            errorInput.Append(
                "This converter cannot handle negative values with adequate precision. Use the IEEE converter if handling negative decimal values.\n");
            checks[2] = true;
        }
        //Check is there are letters or illegal symbols
        foreach (var c in input)
        {
            if (!char.IsDigit(c) && !c.Equals('.') && !c.Equals(' '))
            {
                checks[3] = true;
            }
        }

        if (checks[3])
            errorInput.Append("Decimal numbers cannot contain letters or illegal symbols.\n");

        if (!checks[0] && !checks[1] && !checks[2] && !checks[3])
            isValid = true;

        if (isValid)
        {
            foreach (var c in input.Where(c => char.IsDigit(c) || c.Equals('.')))
            {
                cleanedInput.Append(c);
            }
        }


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
        throw new NotImplementedException();
    }

    

    #endregion
}