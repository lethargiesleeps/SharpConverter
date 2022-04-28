using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Contracts;

internal interface INumberSystemConverter
{
    string DecimalToBinary(string decimalValue);
    string DecimalToOctal(string decimalValue);
    string DecimalToHexadecimal(string decimalValue);
    string BinaryToDecimal(string binaryValue);
    string BinaryToOctal(string binaryValue);
    string BinaryToHexadecimal(string binaryValue);
    string HexadecimalToDecimal(string hexValue);
    string HexadecimalToOctal(string hexValue);
    string HexadecimalToBinary(string hexValue);
    string OctalToBinary(string octalValue);
    string OctalToDecimal(string octalValue);
    string OctalToHexadecimal(string octalValue);
    string ReturnOriginalValue(CommandState commandState, string input);
}