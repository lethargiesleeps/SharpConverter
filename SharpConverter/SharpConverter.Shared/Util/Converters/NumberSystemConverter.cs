using SharpConverter.Shared.Util.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util.Converters
{

    public class NumberSystemConverter : INumberSystemConverter
    {
        private const byte MaxDecimalInputSize = 100;
        public string BinaryToDecimal(string binaryValue)
        {
            throw new NotImplementedException();
        }

        //TODO: Use string builder to minimize memory impact
        public string DecimalToBinary(string decimalValue)
        {
            //Return string
            string returnValue = "";
            string beforeSplit = "";
            string afterSplit = "";
            int preDecimal = 0;
            int postDecimal = 0;
            int index = 0;
            int[] numberArray = new int[MaxDecimalInputSize];
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            int afterDivision = 0;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            bool numValidated = false;
            bool containsFloatingPoint = false;

            #region InputValidationLogic
            //Check if empty, null or whitespace
            if (string.IsNullOrWhiteSpace(decimalValue))
                returnValue = "Enter a decimal value. (Numbers containing 0-9 and/or decimal values, try to omit spaces for this conversion)";
            if (string.IsNullOrEmpty(decimalValue))
                returnValue = "Value is empty.";

            //Check if all digits or decimal, switch if number has floating value
            int pointCounter = 0; //Tracks if there are more than one decimal point in input
            foreach (var c in decimalValue.ToCharArray())
            {
                if (!char.IsDigit(c) && c != '.')
                    returnValue = "Enter a decimal value. (Numbers containing 0-9 and/or decimal values.)";
                else
                    numValidated = true;

                if(c == '.')
                {
                    containsFloatingPoint = true;
                    pointCounter++;
                }

                //Finally, throw if there is more than one point
                if(pointCounter > 1)
                {
                    numValidated = false; //Incase switched to true previously
                    returnValue = "Decimal number cannot contain more than one point.";
                }
                
            }

            //Check if user tries to input negative value
            if (decimalValue[0] == '-')
            {
                returnValue =
                    "Try to enter positive values only. For accurate depiction of negative values, use the IEEE754 conversion tool";
                numValidated = false;
            }
            //Ensure number is no greater than 100
            if (decimalValue.Length >= MaxDecimalInputSize)
            {
                returnValue =
                    "Keep your conversion under 100 digits, for more precise conversion use the IEEE754 conversion tool.";
                numValidated = false;
            }
            #endregion

            #region SplitInputAndConversionn
            if (numValidated)
            {
                //Splits the input into two strings, and their corresponding integer values
                if (containsFloatingPoint)
                {
                    beforeSplit = Tools.SplitDecimal(false, decimalValue);
                    afterSplit = Tools.SplitDecimal(true, decimalValue);
                    preDecimal = int.Parse(beforeSplit);
                    postDecimal = int.Parse(afterSplit);
                }
                //Simply Converts input value to integer and string
                else
                {
                    beforeSplit = decimalValue;
                    preDecimal = int.Parse(decimalValue);
                }
                    
            }
            else
                return returnValue;
            #endregion

            #region BinaryConversionLogic

            if (numValidated)
            {
                for (index = 0; preDecimal > 0; index++)
                {
                    numberArray[index] = preDecimal % 2;
                    preDecimal /= 2;
                }

                //TODO: If has floating value logic
                if (containsFloatingPoint) { }
            }
            #endregion

            #region ToStringLogic

            if (numValidated)
            {
                returnValue = string.Format("{0} in binary is: \n", decimalValue);
                for (int i = index - 1; i >= 0; i--)
                {
                    returnValue += numberArray[i];
                }
            }
            #endregion

            return returnValue;

        }

        public string DecimalToHexadecimal(string decimalValue)
        {
            StringBuilder returnValue = new StringBuilder();
            string beforeSplit = string.Empty;
            string afterSplit = string.Empty;


            bool numValidated = false;
            bool containsFloatingPoint = false;

            int index = 0;
            int[] numberArray = new int[MaxDecimalInputSize];
            int wholeNumber;
            int floatingPointNumber;

            #region InputValidationLogic
            //Check if empty, null or whitespace
            if (string.IsNullOrWhiteSpace(decimalValue))
                returnValue.Append(
                    "Enter a decimal value. (Numbers containing 0-9 and/or decimal values, try to omit any spaces for this conversion).");
            if (string.IsNullOrEmpty(decimalValue))
                returnValue.Append("Values is empty.");

            

            //Check if value is all digits or decimal, switch if number has floating values
            int pointCounter = 0;
            foreach (var c in decimalValue)
            {
                if (!char.IsDigit(c) && c != '.')
                    returnValue.Append("Enter a decimal value. (Numbers containing 0-9 and/or decimal values.)");
                else
                    numValidated = true;

                if (c == '.')
                {
                    containsFloatingPoint = true;
                    pointCounter++;
                }

                //Throw if more than one point
                if (pointCounter > 1)
                {
                    numValidated = false; //Incase switched to true before
                    returnValue.Append("Decimal number cannot contain more than one point.");
                }
            }

            //Check if user tries to input negative value
            if (decimalValue[0] == '-')
            {
                returnValue.Append(
                    "Try to enter positive values only. For accurate depiction of negative values, use the IEEE754 conversion tool");
                numValidated = false;
            }
            //Ensure number is no greater than 100
            if (decimalValue.Length >= MaxDecimalInputSize)
            {
                returnValue.Append(
                    "Keep your conversion under 100 digits, for more precise conversion use the IEEE754 conversion tool.");
                numValidated = false;
            }
                
            #endregion

            #region SplitInputAndConversion

            if (numValidated)
            {
                if (containsFloatingPoint)
                {
                    beforeSplit = Tools.SplitDecimal(false, decimalValue);
                    afterSplit = Tools.SplitDecimal(true, decimalValue);
                    wholeNumber = int.Parse(beforeSplit);
                    floatingPointNumber = int.Parse(afterSplit);
                }
                else
                {
                    beforeSplit = decimalValue;
                    wholeNumber = int.Parse(decimalValue);
                }
            }
            else
                return returnValue.ToString();
            #endregion

            #region HexConversionLogic

            if (numValidated)
            {
                
                for (index = 0; wholeNumber > 0; index++)
                {
                    numberArray[index] = wholeNumber % 16;
                    wholeNumber /= 16;
                }
                //TODO: If has floating value logic
                if (containsFloatingPoint) { }
            }

            #endregion

            #region ToStringLogic

            if (numValidated)
            {
                returnValue.Append(string.Format("{0} in hexadecimal is: \n", decimalValue));
                for (int i = index - 1; i >= 0; i--)
                {
                    if (numberArray[i] >= 10)
                        returnValue.Append(GetHexValues()[numberArray[i]]);
                    else
                        returnValue.Append(numberArray[i]);
                }
            }

            #endregion

            return returnValue.ToString();
        }

        //TODO: Use string builder to minimize memory impact
        public string DecimalToOctal(string decimalValue)
        {
            //Return string
            string returnValue = "";
            string beforeSplit = "";
            string afterSplit = "";

            int preDecimal = 0;
            int postDecimal = 0;
            int afterDivision = 0;
            int[] numberArray = new int[MaxDecimalInputSize];
            int index = 0;

            bool numValidated = false;
            bool containsFloatingPoint = false;

            #region InputValidationLogic
            //Check if empty, null or whitespace
            if (string.IsNullOrWhiteSpace(decimalValue))
                returnValue = "Enter a decimal value. (Numbers containing 0-9 and/or decimal values, try to omit spaces for this conversion)";
            if (string.IsNullOrEmpty(decimalValue))
                returnValue = "Value is empty.";

            

            //Check if all digits or decimal, switch if number has floating value
            int pointCounter = 0; //Tracks if there are more than one decimal point in input
            foreach (var c in decimalValue.ToCharArray())
            {
                if (!char.IsDigit(c) && c != '.')
                    returnValue = "Enter a decimal value. (Numbers containing 0-9 and/or decimal values.)";
                else
                    numValidated = true;

                if (c == '.')
                {
                    containsFloatingPoint = true;
                    pointCounter++;
                }

                //Finally, throw if there is more than one point
                if (pointCounter > 1)
                {
                    numValidated = false; //Incase switched to true previously
                    returnValue = "Decimal number cannot contain more than one point.";
                }

            }

            if (decimalValue[0] == '-')
            {
                returnValue =
                    "Try to enter positive values only. For accurate depiction of negative values, use the IEEE754 conversion tool";
                numValidated = false;
            }
            //Ensure number is no greater than 100
            if (decimalValue.Length >= MaxDecimalInputSize)
            {
                returnValue =
                    "Keep your conversion under 100 digits, for more precise conversion use the IEEE754 conversion tool.";
                numValidated = false;
            }
            #endregion

            #region SplitInputAndConversionn
            if (numValidated)
            {
                //Splits the input into two strings, and their corresponding integer values
                if (containsFloatingPoint)
                {
                    beforeSplit = Tools.SplitDecimal(false, decimalValue);
                    afterSplit = Tools.SplitDecimal(true, decimalValue);
                    preDecimal = int.Parse(beforeSplit);
                    postDecimal = int.Parse(afterSplit);
                }
                //Simply Converts input value to integer and string
                else
                {
                    beforeSplit = decimalValue;
                    preDecimal = int.Parse(decimalValue);
                }

            }
            else
                return returnValue;
            #endregion

            #region OctalConversionLogic


            if (numValidated)
            {
                for (index = 0; preDecimal > 0; index++)
                {
                    numberArray[index] = preDecimal % 8;
                    preDecimal /= 8;
                }
            }

            //TODO: If has floating value logic
            if (containsFloatingPoint) { }
            #endregion

            #region ToStringLogic

            if (numValidated)
            {
                returnValue = string.Format("{0} in octal is: \n", decimalValue);
                for (int i = index - 1; i >= 0; i--)
                {
                    returnValue += numberArray[i];
                }
            }
            #endregion

            return returnValue;
        }

        public char[] GetHexValues()
        {
            char[] hexValues = new char[16]
            {
                '0', '1', '2', '3',
                '4', '5', '6', '7',
                '8', '9', 'A', 'B',
                'C', 'D', 'E', 'F'

            };
            return hexValues;
        }

        public string HexadecimalToDecimal(string hexValue)
        {
            throw new NotImplementedException();
        }

        public string MultiConverted(string from, string to)
        {
            throw new NotImplementedException();
        }

        public string ReturnOriginalValue(string input)
        {
            string returnValue = "";

            if (input == null)
                returnValue = "Input was null.";

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var c in input)
            {
                if (!char.IsDigit(c))
                    returnValue = "Input not a number. No conversion is possible";
                else
                    returnValue += c;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return "Result is same as input:\n" + returnValue;

        }

        public string OctalToDecimal(string octalValue)
        {
            throw new NotImplementedException();
        }

    }
}
