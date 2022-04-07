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

            //Check if empty or whitespace
            if (string.IsNullOrWhiteSpace(decimalValue))
                returnValue = "Enter a decimal value. (Numbers containing 0-9 and/or decimal values, try to ommit spaces for this conversion)";

            if (string.IsNullOrEmpty(decimalValue))
                returnValue = "Value is empty.";

            foreach (var c in decimalValue.ToCharArray())
            {
                if (!char.IsDigit(c) && c != '.')
                    returnValue = "Enter a decimal value. (Numbers containing 0-9 and/or decimal values.)";
            }
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
