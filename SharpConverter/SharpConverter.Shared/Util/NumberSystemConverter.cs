using SharpConverter.Shared.Util.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util
{
    public class NumberSystemConverter : INumberSystemConverter
    {
        public string BinaryToDecimal(string binaryValue)
        {
            throw new NotImplementedException();
        }

        public string DecimalToBinary(string decimalValue)
        {
            //Return string
            string returnValue = "";
            string beforeSplit = "";
            string afterSplit = "";
            int preDecimal = 0;
            int postDecimal = 0;
            int afterDivision = 0;
            bool numValidated = false;
            bool containsFloatingPoint = false;

            #region InputValidationLogic
            //Check if empty, null or whitespace
            if (string.IsNullOrWhiteSpace(decimalValue))
                returnValue = "Enter a decimal value. (Numbers containing 0-9 and/or decimal values, try to ommit spaces for this conversion)";
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

            int[] numberArray = new int[preDecimal];
            int index;
            for (index = 0; preDecimal > 0; index++)
            {
                numberArray[index] = preDecimal % 2;
                preDecimal /= 2;
            }

            //TODO: If has floating value logic
            if (containsFloatingPoint) { }
            #endregion

            #region ToStringLogic
            returnValue = string.Format("{0} in binary is: \n", decimalValue);
            for(int i = index - 1; i >= 0; i--)
            {
                returnValue += numberArray[i];
            }
            #endregion

            return returnValue;

        }

        public string DecimalToHexadecimal(string decimalValue)
        {
            throw new NotImplementedException();
        }

        public string DecimalToOctal(string decimalValue)
        {
            throw new NotImplementedException();
        }

        public char[] GetHexValues()
        {
            throw new NotImplementedException();
        }

        public string HexadecimalToDecimal(string hexValue)
        {
            throw new NotImplementedException();
        }

        public string MultiConverted(string from, string to)
        {
            throw new NotImplementedException();
        }

        public string OctalToDecimal(string octalValue)
        {
            throw new NotImplementedException();
        }

    }
}
