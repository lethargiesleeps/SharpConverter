using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Contracts
{
    internal interface INumberSystemConverter
    {
        string DecimalToBinary(string decimalValue);
        string BinaryToDecimal(string binaryValue);
        string DecimalToHexadecimal(string decimalValue);
        string HexadecimalToDecimal(string hexValue);

        string BinaryToOctal(string binaryValue);
        string OctalToBinary(string octalValue);
        string BinaryToHexadecimal(string binaryValue);
        string HexadecimalToBinary(string hexValue);

        string DecimalToOctal(string decimalValue);
        string OctalToDecimal(string octalValue);
        string ReturnOriginalValue(string input);
        
    }
}
