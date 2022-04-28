using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Contracts;

/*
 * Class responsible for handling passed arguments and returning appropriate strings as final output.
 * All methods needs the ArgumentState, the state of which conversion is being applied (the 'to' of the conversion)
 * and a string 'nscOutput' which is the current, cleaned, valid conversion obtained from class NumberSystemConverter.
 *
 * If nscOutput contains "ERROR", the methods break and only the error message generated from NumberSystemConverter is returned.
 *
 */
public interface IArgumentHandler
{
    void SetArgumentState(ArgumentState argumentState);
    string SplitByByte(ArgumentState state, string nscOutput);
    string SplitByWord(ArgumentState state, string nscOutput);
    string SplitByNibble(ArgumentState state, string nscOutput);
    string TernaryState(ArgumentState state, string nscOutput);
    string TrailingZeroPrefix(ArgumentState state, string nscOutput);
    string GetOnesComplement(ArgumentState state, string nscOutput);
    string GetTwosComplement(ArgumentState state, string nscOutput);
    string ConvertAll(CommandState state, string originalValue);
}