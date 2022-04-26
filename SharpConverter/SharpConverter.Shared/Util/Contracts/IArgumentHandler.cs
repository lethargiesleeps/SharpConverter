using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Contracts;

public interface IArgumentHandler
{
    string SplitByByte(ArgumentState state);
    string SplitByWord(ArgumentState state);
    string SplitByNibble(ArgumentState state);
    string GetOnesComplement(ArgumentState state);
    string GetTwosComplement(ArgumentState state);
    string ConvertAll(ArgumentState state);
}