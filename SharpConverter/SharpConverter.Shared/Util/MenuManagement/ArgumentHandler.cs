using System.Text;
using SharpConverter.Shared.Util.Contracts;
using SharpConverter.Shared.Util.Converters;
using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.MenuManagement;

/*
 *Implements IArgumentHandler. See that interface for more details, or view comments in methods for return logic
 * This class ASSUMES that all string arguments have passed the Validation methods of NumberSystemConverter.
 * Any implementation of this class should only be used if a CommandState is not in Error state or Default state
 */
public class ArgumentHandler : IArgumentHandler
{
    private readonly NumberSystemConverter _numberSystemConverter;

    public ArgumentHandler(ArgumentState argumentState, NumberSystemConverter numberSystemConverter)
    {
        ArgumentState = argumentState;
        _numberSystemConverter = numberSystemConverter;
    }

    public ArgumentState ArgumentState { get; private set; }
    public byte MaxArguments => 8;

    public void SetArgumentState(ArgumentState argumentState)
    {
        ArgumentState = argumentState;
    }

    public string SplitByByte(ArgumentState state, string nscOutput)
    {
        throw new NotImplementedException();
    }

    public string SplitByWord(ArgumentState state, string nscOutput)
    {
        throw new NotImplementedException();
    }

    public string SplitByNibble(ArgumentState state, string nscOutput)
    {
        throw new NotImplementedException();
    }

    public string TernaryState(ArgumentState state, string nscOutput)
    {
        throw new NotImplementedException();
    }

    public string TrailingZeroPrefix(ArgumentState state, string nscOutput)
    {
        throw new NotImplementedException();
    }

    public string GetOnesComplement(ArgumentState state, string nscOutput)
    {
        var returnString = new StringBuilder();
        var conversionHolder = new StringBuilder[2]
        {
            new(), new()
        };
        switch (state)
        {
            case ArgumentState.Binary:
                for (var i = 0; i < nscOutput.Length; i++)
                    switch (nscOutput[i])
                    {
                        case '0':
                            conversionHolder[0].Append('1');
                            break;
                        case '1':
                            conversionHolder[0].Append('0');
                            break;
                        default:
                            conversionHolder[0].Append(nscOutput[i]);
                            break;
                    }

                returnString.Append(conversionHolder[0].Equals("0")
                    ? "1"
                    : _numberSystemConverter.ReturnOriginalValue(CommandState.BinaryToBinary,conversionHolder[0].ToString()));

                break;
            case ArgumentState.Decimal:
                conversionHolder[0].Append(_numberSystemConverter.DecimalToBinary(nscOutput));
                for (var i = 0; i < conversionHolder[0].Length; i++)
                    switch (conversionHolder[0][i])
                    {
                        case '0':
                            conversionHolder[1].Append('1');
                            break;
                        case '1':
                            conversionHolder[1].Append('0');
                            break;
                        default:
                            conversionHolder[1].Append(conversionHolder[0][i]);
                            break;
                    }

                returnString.Append(_numberSystemConverter.BinaryToDecimal(conversionHolder[1].ToString()));
                break;
            case ArgumentState.Hexadecimal:
                conversionHolder[0].Append(_numberSystemConverter.HexadecimalToBinary(nscOutput));
                for (var i = 0; i < conversionHolder[0].Length; i++)
                    switch (conversionHolder[0][i])
                    {
                        case '0':
                            conversionHolder[1].Append('1');
                            break;
                        case '1':
                            conversionHolder[1].Append('0');
                            break;
                        default:
                            conversionHolder[1].Append(conversionHolder[0][i]);
                            break;
                    }

                returnString.Append(conversionHolder[1].Equals("0000")
                    ? "0"
                    : _numberSystemConverter.BinaryToHexadecimal(conversionHolder[1].ToString()));
                break;
            case ArgumentState.Octal:
                conversionHolder[0].Append(_numberSystemConverter.OctalToBinary(nscOutput));
                for (var i = 0; i < conversionHolder[0].Length; i++)
                    switch (conversionHolder[0][i])
                    {
                        case '0':
                            conversionHolder[1].Append('1');
                            break;
                        case '1':
                            conversionHolder[1].Append('0');
                            break;
                        default:
                            conversionHolder[1].Append(conversionHolder[0][i]);
                            break;
                    }

                returnString.Append(conversionHolder[1].Equals("000")
                    ? "0"
                    : _numberSystemConverter.BinaryToOctal(conversionHolder[1].ToString()));
                break;
            case ArgumentState.Default:
                return string.Empty;
            case ArgumentState.Error:
                return string.Empty;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        return returnString.ToString();
    }

    public string GetTwosComplement(ArgumentState state, string nscOutput)
    {
        if (nscOutput.Contains('.'))
            return "ERROR:\nCannot give two's complement of a floating point value.";

        var returnString = new StringBuilder();
        var conversionHolders = new StringBuilder[2] {new(), new()};
        var result = "";
        switch (state)
        {
            case ArgumentState.Binary:
                for (var i = 0; i < nscOutput.Length; i++)
                    switch (nscOutput[i])
                    {
                        case '0':
                            conversionHolders[0].Append('1');
                            break;
                        case '1':
                            conversionHolders[0].Append('0');
                            break;
                        default:
                            conversionHolders[0].Append(nscOutput[i]);
                            break;
                    }

                returnString.Append(Calculator.AddBinary(conversionHolders[0].ToString(), "1"));
                break;
            case ArgumentState.Decimal:
                conversionHolders[0].Append(_numberSystemConverter.DecimalToBinary(nscOutput));
                for (var i = 0; i < conversionHolders[0].Length; i++)
                    switch (conversionHolders[0][i])
                    {
                        case '0':
                            conversionHolders[1].Append('1');
                            break;
                        case '1':
                            conversionHolders[1].Append('0');
                            break;
                        default:
                            conversionHolders[1].Append(conversionHolders[0][i]);
                            break;
                    }

                result = Calculator.AddBinary(conversionHolders[1].ToString(), "1");
                returnString.Append(_numberSystemConverter.BinaryToDecimal(result));
                break;
            case ArgumentState.Hexadecimal:
                conversionHolders[0].Append(_numberSystemConverter.HexadecimalToBinary(nscOutput));
                for (var i = 0; i < conversionHolders[0].Length; i++)
                    switch (conversionHolders[0][i])
                    {
                        case '0':
                            conversionHolders[1].Append('1');
                            break;
                        case '1':
                            conversionHolders[1].Append('0');
                            break;
                        default:
                            conversionHolders[1].Append(conversionHolders[0][i]);
                            break;
                    }

                result = Calculator.AddBinary(conversionHolders[1].ToString(), "1");
                returnString.Append(_numberSystemConverter.BinaryToHexadecimal(result));
                break;
            case ArgumentState.Octal:
                conversionHolders[0].Append(_numberSystemConverter.OctalToBinary(nscOutput));
                for (var i = 0; i < conversionHolders[0].Length; i++)
                    switch (conversionHolders[0][i])
                    {
                        case '0':
                            conversionHolders[1].Append('1');
                            break;
                        case '1':
                            conversionHolders[1].Append('0');
                            break;
                        default:
                            conversionHolders[1].Append(conversionHolders[0][i]);
                            break;
                    }

                result = Calculator.AddBinary(conversionHolders[1].ToString(), "1");
                returnString.Append(_numberSystemConverter.BinaryToOctal(result));
                break;
            case ArgumentState.Default:
                return string.Empty;
            case ArgumentState.Error:
                return string.Empty;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        return returnString.ToString();
    }

    public string ConvertAll(CommandState state, string originalValue)
    {
        var result = new StringBuilder();
        switch (state)
        {
            case CommandState.DecimalToBinary:
            case CommandState.DecimalToOctal:
            case CommandState.DecimalToHexadecimal:
            case CommandState.DecimalToDecimal:
                result.Append($"BINARY: {_numberSystemConverter.DecimalToBinary(originalValue)}\n");
                result.Append($"OCTAL: {_numberSystemConverter.DecimalToOctal(originalValue)}\n");
                result.Append($"HEXADECIMAL: {_numberSystemConverter.DecimalToHexadecimal(originalValue)}\n");
                break;
            case CommandState.BinaryToDecimal:
            case CommandState.BinaryToOctal:
            case CommandState.BinaryToHexadecimal:
            case CommandState.BinaryToBinary:
                result.Append($"DECIMAL: {_numberSystemConverter.BinaryToDecimal(originalValue)}\n");
                result.Append($"OCTAL: {_numberSystemConverter.BinaryToOctal(originalValue)}\n");
                result.Append($"HEXADECIMAL: {_numberSystemConverter.BinaryToHexadecimal(originalValue)}\n");
                break;
            case CommandState.OctalToDecimal:
            case CommandState.OctalToBinary:
            case CommandState.OctalToHexadecimal:
            case CommandState.OctalToOctal:
                result.Append($"DECIMAL: {_numberSystemConverter.OctalToDecimal(originalValue)}\n");
                result.Append($"BINARY: {_numberSystemConverter.OctalToBinary(originalValue)}\n");
                result.Append($"HEXADECIMAL: {_numberSystemConverter.OctalToHexadecimal(originalValue)}\n");
                break;
            case CommandState.HexadecimalToDecimal:
            case CommandState.HexadecimalToBinary:
            case CommandState.HexadecimalToOctal:
            case CommandState.HexadecimalToHexadecimal:
                result.Append($"DECIMAL: {_numberSystemConverter.HexadecimalToDecimal(originalValue)}\n");
                result.Append($"BINARY: {_numberSystemConverter.HexadecimalToBinary(originalValue)}\n");
                result.Append($"OCTAL: {_numberSystemConverter.HexadecimalToOctal(originalValue)}\n");
                break;
            case CommandState.Error:
                break;
            case CommandState.Help:
                break;
            case CommandState.Exit:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        return result.ToString();
    }
}