using System.Globalization;
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
        ulong conversionTest = 0;
        var finalConversion = new StringBuilder();
        ulong beforeFloatingPoint = 0;
        ulong afterFloatingPoint = 0;

        var input = ValidateDecimalInput(decimalValue);


        var isValid = !input.Contains("ERROR");


        if (isValid)
        {
            //TODO:Handle floating points
            if (input.Contains('.'))
            {
                beforeFloatingPoint = ulong.Parse(Tools.SplitDecimal(false, input));
                afterFloatingPoint = ulong.Parse(Tools.SplitDecimal(true, input));
            }
            else
            {
                beforeFloatingPoint = ulong.Parse(input);
                var binaryArray = new ulong[byte.MaxValue];
                for (var i = 0; beforeFloatingPoint > 0; i++)
                {
                    binaryArray[i] = beforeFloatingPoint % 2;
                    beforeFloatingPoint /= 2;
                    finalConversion.Append(binaryArray[i]);
                }

                finalConversion = Tools.ReverseString(finalConversion);
            }

            if (finalConversion.Equals("") || finalConversion.Equals(" "))
                return "0";

            return finalConversion.ToString();
        }

        return input; //Returns error message
    }

    public string BinaryToDecimal(string binaryValue)
    {
        var input = ValidateBinaryInput(binaryValue);
        //Validation
        var isValid = !input.Contains("ERROR");


        //Conversion Logic
        if (isValid)
        {
            //TODO: Handle floating points
            if (input.Contains('.')) return input;

            double decimalValue = 0;
            var exponent = 0;
            for (var i = input.Length - 1; i >= 0; i--)
            {
                decimalValue += double.Parse(input[i].ToString()) * Math.Pow(2, exponent);
                exponent++;
            }

            if (input.Equals("") || input.Equals(" ") || decimalValue.ToString().Equals("") ||
                decimalValue.ToString().Equals(" "))
                return "0";
            return decimalValue.ToString(CultureInfo.InvariantCulture);
        }

        return input; //Returns error message
    }

    public string DecimalToHexadecimal(string decimalValue)
    {
        //Declarations
        ulong conversionTest = 0;
        var finalConversion = new StringBuilder();
        ulong beforeFloatingPoint = 0;
        ulong afterFloatingPoint = 0;
        var input = ValidateDecimalInput(decimalValue);
        var isValid = !input.Contains("ERROR");
        if (input.Equals("0"))
            return "0";


        //Conversion logic
        if (isValid)
        {
            //TODO:Handle floating points
            if (input.Contains('.'))
            {
                beforeFloatingPoint = ulong.Parse(Tools.SplitDecimal(false, input));
                afterFloatingPoint = ulong.Parse(Tools.SplitDecimal(true, input));
            }
            else
            {
                beforeFloatingPoint = ulong.Parse(input);
                var hexArray = new ulong[byte.MaxValue];
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
        var input = ValidateHexadecimalInput(hexValue);
        var isValid = !input.Contains("ERROR");

        if (isValid)
        {
            //TODO: Handle floating points
            if (input.Contains('.')) return input;
            ulong decimalValue = 0;
            var exponent = 0;
            for (var i = input.Length - 1; i >= 0; i--)
            {
                var realValue = 0;
                switch (input[i])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        realValue = int.Parse(input[i].ToString());
                        break;
                    case 'A':
                        realValue = 10;
                        break;
                    case 'B':
                        realValue = 11;
                        break;
                    case 'C':
                        realValue = 12;
                        break;
                    case 'D':
                        realValue = 13;
                        break;
                    case 'E':
                        realValue = 14;
                        break;
                    case 'F':
                        realValue = 15;
                        break;
                }

                decimalValue += (ulong) (realValue * Math.Pow(16, exponent));
                exponent++;
            }

            if (input.Equals("") || input.Equals(" ") || decimalValue.ToString().Equals("") ||
                decimalValue.ToString().Equals(" "))
                return "0";
            return decimalValue.ToString();
        }

        return input; //Returns error message
    }

    public string HexadecimalToOctal(string hexValue)
    {
        var input = ValidateHexadecimalInput(hexValue);
        var isValid = !input.Contains("ERROR");
        if (!isValid) return input; //Returns error message
        //TODO:Handle floating points
        if (input.Contains('.')) return input; //To change
        var decimalValue = HexadecimalToDecimal(input);
        return DecimalToOctal(decimalValue);
    }

    public string BinaryToOctal(string binaryValue)
    {
        var input = ValidateBinaryInput(binaryValue);
        //Validation
        var isValid = !input.Contains("ERROR");

        //Conversion Logic => Uses previously defined method BinaryToDecimal() to facilitate process
        //Might make an alternative method using array counting once bulk of project is completed
        if (!isValid) return input;
        //TODO: Handle floating points
        if (input.Contains('.')) return input; //To change

        var decimalValue = BinaryToDecimal(input);
        return DecimalToOctal(decimalValue);
    }

    public string OctalToBinary(string octalValue)
    {
        var input = ValidateOctalInput(octalValue);
        var isValid = !input.Contains("ERROR");

        //Conversion Logic => Uses previously defined method OctalToDecimal() to facilitate process
        //Might make an alternative method using array counting once bulk of project is completed

        if (!isValid) return input; //Returns error message
        //TODO:Handle floating points
        if (input.Contains('.')) return input; //To change

        var decimalValue = OctalToDecimal(input);
        return DecimalToBinary(decimalValue);
    }

    public string BinaryToHexadecimal(string binaryValue)
    {
        var input = ValidateBinaryInput(binaryValue);
        //Validations
        var isValid = !input.Contains("ERROR");

        //Conversion Logic => Uses previously defined method BinaryToDecimal() to facilitate process
        //Might make an alternative method using array counting once bulk of project is completed
        //This method works for now, although less fun than creating its own logic

        if (!isValid) return input; //Returns error message
        //TODO: handle floating points
        if (input.Contains('.')) return input; //To change
        var decimalValue = BinaryToDecimal(input);
        return DecimalToHexadecimal(decimalValue);
    }

    public string HexadecimalToBinary(string hexValue)
    {
        var input = ValidateHexadecimalInput(hexValue);
        var isValid = !input.Contains("ERROR");
        if (!isValid) return input; //Returns error message
        //TODO: Handle floating points
        if (input.Contains('.')) return input; //To change
        var decimalValue = HexadecimalToDecimal(input);
        return DecimalToBinary(decimalValue);
    }

    public string DecimalToOctal(string decimalValue)
    {
        ulong conversionTest = 0;
        var finalConversion = new StringBuilder();
        ulong beforeFloatingPoint = 0;
        ulong afterFloatingPoint = 0;
        var input = ValidateDecimalInput(decimalValue);
        var isValid = !input.Contains("ERROR");
        if (input.Equals("0"))
            return "0";


        if (isValid)
        {
            //TODO:Handle floating points
            if (input.Contains('.'))
            {
                beforeFloatingPoint = ulong.Parse(Tools.SplitDecimal(false, input));
                afterFloatingPoint = ulong.Parse(Tools.SplitDecimal(true, input));
            }
            else
            {
                beforeFloatingPoint = ulong.Parse(input);
                var octalArray = new ulong[byte.MaxValue];
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
        var input = ValidateOctalInput(octalValue);
        //Validation
        var isValid = !input.Contains("ERROR");

        //Conversion logic
        if (isValid)
        {
            //TODO: Handle floating points
            if (input.Contains('.')) return input;

            double decimalValue = 0;
            var exponent = 0;
            for (var i = input.Length - 1; i >= 0; i--)
            {
                decimalValue += double.Parse(input[i].ToString()) * Math.Pow(8, exponent);
                exponent++;
            }

            return decimalValue.ToString(CultureInfo.InvariantCulture);
        }

        return input; // Returns error message
    }

    public string OctalToHexadecimal(string octalValue)
    {
        var input = ValidateOctalInput(octalValue);
        //Validation
        var isValid = !input.Contains("ERROR");
        if (!isValid) return input; //Returns error message
        //TODO: Handle floating points
        if (input.Contains('.')) return input; //To change
        var decimalValue = OctalToDecimal(input);
        return DecimalToHexadecimal(decimalValue);
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
        //5 = Max Length
        var checks = new bool[6]
        {
            false, false, false,
            false, false, false
        };

        return checks;
    }

    public string ReturnOriginalValue(CommandState commandState, string input)
    {
        var returnString = new StringBuilder();
        switch (commandState)
        {
            case CommandState.BinaryToBinary:
                returnString.Append(ValidateBinaryInput(input));
                break;
            case CommandState.HexadecimalToHexadecimal:
                returnString.Append(ValidateHexadecimalInput(input));
                break;
            case CommandState.DecimalToDecimal:
                returnString.Append(ValidateDecimalInput(input));
                break;
            case CommandState.OctalToOctal:
                returnString.Append(ValidateOctalInput(input));
                break;
            default:
                throw new ArgumentException(
                    "Something went wrong when passing argument commandState of method ReturnOriginalValue() in class NumberSystemConverter.cs");
        }

        return returnString.ToString();
    }

    #endregion


    #region Validation

    //TODO: Fix messages for > maxLength
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
            if (input[0].Equals('-'))
            {
                errorInput.Append(
                    "This converter cannot handle negative values with adequate precision. Use the IEEE converter if handling negative decimal values.\n");
                checks[2] = true;
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
        var errorInput = new StringBuilder();
        var cleanedInput = new StringBuilder();
        var numberOfFloatingPoints = 0;
        var hasFloatingPoints = false;
        var isValid = false;
        var checks = ValidationChecks();


        //Error message instantiation
        errorInput.Append("ERROR:\n");
        //Check null, empty, whitespace
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(input))
        {
            errorInput.Append("Input cannot be empty or whitespace.\n");
            checks[0] = true;
        }

        //Floating point check
        if (input.Contains('.'))
            hasFloatingPoints = true;
        if (hasFloatingPoints) numberOfFloatingPoints += input.Count(i => i.Equals('.'));

        if (numberOfFloatingPoints > 1)
        {
            errorInput.Append("Input has too many floating points. Can only contain 1 floating point.\n");
            checks[1] = true;
        }

        //Negative number check
        if (input.Length > 0)
            if (input[0].Equals('-'))
            {
                errorInput.Append(
                    "This converter cannot handle binary values with '-'. Use the IEEE converter if handling negative binary values.\n");
                checks[2] = true;
            }

        //Max Value
        if (input.Length > 50)
        {
            errorInput.Append("Cannot enter a value greater than 50 binary digits.\n");
            checks[5] = true;
        }

        //Check is there are letters or illegal symbols
        foreach (var c in input.Where(c => !char.IsDigit(c) && !c.Equals('.') && !c.Equals(' ')))
            checks[3] = true;

        if (checks[3])
            errorInput.Append("Decimal numbers cannot contain letters or illegal symbols.\n");


        //Check for illegal numbers
        var isValidCharacter = new bool[input.Length];
        for (var i = 0; i < input.Length; i++)
            switch (input[i])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '.':
                case ' ':
                    isValidCharacter[i] = true;
                    break;
                default:
                    isValidCharacter[i] = false;
                    break;
            }

        foreach (var b in isValidCharacter)
            if (!b)
                checks[4] = true;


        if (checks[4])
            errorInput.Append("Octal values must be between 0 and 7.\n");

        if (!checks[0] && !checks[1] && !checks[2] && !checks[3] && !checks[4] && !checks[5])
            isValid = true;

        if (isValid)
            foreach (var c in input.Where(c => char.IsDigit(c) || c.Equals('.')))
                cleanedInput.Append(c);

        errorInput.Append("Press any key to try again...");

        //Final result
        return isValid ? cleanedInput.ToString() : errorInput.ToString();
    }

    public string ValidateHexadecimalInput(string input)
    {
        var errorInput = new StringBuilder();
        var cleanedInput = new StringBuilder();
        var numberOfFloatingPoints = 0;
        var hasFloatingPoints = false;
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
            hasFloatingPoints = true;
        if (hasFloatingPoints) numberOfFloatingPoints += input.Count(i => i.Equals('.'));

        if (numberOfFloatingPoints > 1)
        {
            errorInput.Append("Input has too many floating points. Can only contain 1 floating point.\n");
            checks[1] = true;
        }

        //Negative number check
        if (input.Length > 0)
            if (input[0].Equals('-'))
            {
                errorInput.Append(
                    "This converter cannot handle binary values with '-'. Use the IEEE converter if handling negative binary values.\n");
                checks[2] = true;
            }

        //Max Value
        if (input.Length > 50)
        {
            errorInput.Append("Cannot enter a value greater than 50 binary digits.\n");
            checks[5] = true;
        }

        //Check for illegale characters
        var isValidCharacter = new bool[input.Length];
        for (var i = 0; i < input.Length; i++)
            switch (input[i])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case 'A':
                case 'a':
                case 'B':
                case 'b':
                case 'C':
                case 'c':
                case 'D':
                case 'd':
                case 'E':
                case 'e':
                case 'F':
                case 'f':
                case '.':
                case ' ':
                    isValidCharacter[i] = true;
                    break;
                default:
                    isValidCharacter[i] = false;
                    break;
            }

        foreach (var b in isValidCharacter)
            if (!b)
                checks[4] = true;

        if (checks[4])
            errorInput.Append("Hexadecimal values must be between 0-9 and/or A-F.\n");
        if (!checks[0] && !checks[1] && !checks[2] && !checks[3] && !checks[4] && !checks[5])
            isValid = true;
        if (isValid)
            foreach (var c in input.Where(c => !c.Equals(' ')))
                cleanedInput.Append(c);

        errorInput.Append("Press any key to try again...");

        //Final result
        return isValid ? cleanedInput.ToString().ToUpper() : errorInput.ToString();
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
        if (hasFloatingPoint) numberOfFloatingPoints += input.Count(i => i.Equals('.'));

        if (numberOfFloatingPoints > 1)
        {
            errorInput.Append("Input has too many floating points. Can only contain 1 floating point.\n");
            checks[1] = true;
        }

        //Negative number check
        if (input.Length > 0)
            if (input[0].Equals('-'))
            {
                errorInput.Append(
                    "This converter cannot handle binary values with '-'. Use the IEEE converter if handling negative binary values.\n");
                checks[2] = true;
            }

        //Max Value
        if (input.Length > 50)
        {
            errorInput.Append("Cannot enter a value greater than 50 binary digits.\n");
            checks[5] = true;
        }


        //Check is there are letters or illegal symbols
        foreach (var c in input.Where(c => !char.IsDigit(c) && !c.Equals('.') && !c.Equals(' ')))
            checks[3] = true;

        if (checks[3])
            errorInput.Append("Decimal numbers cannot contain letters or illegal symbols.\n");

        //Check if not a 0 or 1
        foreach (var c in input)
            if (!c.Equals('0') && !c.Equals('1') && !c.Equals(' ') && !c.Equals('.'))
                checks[4] = true;

        if (checks[4])
            errorInput.Append("Binary values can only consist of '0's and '1's.\n");

        if (!checks[0] && !checks[1] && !checks[2] && !checks[3] && !checks[4] && !checks[5])
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