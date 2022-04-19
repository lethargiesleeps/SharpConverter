using System.Diagnostics.CodeAnalysis;
using System.Text;
using SharpConverter.Shared.Util.Contracts;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Converters;

public class NumberSystemConverter : INumberSystemConverter, IInputValidator
{
    public NSCErrorState NscErrorState { get; private set; }
    public string DecimalToBinary(string decimalValue)
    {
        throw new NotImplementedException();
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

    public char[] GetHexValues()
    {
        throw new NotImplementedException();
    }

    public string ReturnOriginalValue(string input)
    {
        throw new NotImplementedException();
    }

    public string MultiConverted(string @from, string to)
    {
        throw new NotImplementedException();
    }

    public bool ValidateDecimalInput(string input)
    {
        throw new NotImplementedException();
    }

    public bool ValidateOctalInput(string input)
    {
        throw new NotImplementedException();
    }

    public bool ValidateHexadecimalInput(string input)
    {
        throw new NotImplementedException();
    }

    public bool ValidateBinaryInput(string input)
    {
        throw new NotImplementedException();
    }

    public string GenerateErrorMessage(NSCErrorState errorState)
    {
        throw new NotImplementedException();
    }
}