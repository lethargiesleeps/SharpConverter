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
                            returnString.Append('1');
                            break;
                        case '1':
                            returnString.Append('0');
                            break;
                        default:
                            returnString.Append(nscOutput[i]);
                            break;
                    }

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

                returnString.Append(_numberSystemConverter.BinaryToHexadecimal(conversionHolder[1].ToString()));
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

                returnString.Append(_numberSystemConverter.BinaryToOctal(conversionHolder[1].ToString()));
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
        throw new NotImplementedException();
    }

    public string ConvertAll(ArgumentState state, string nscOutput)
    {
        throw new NotImplementedException();
    }
}