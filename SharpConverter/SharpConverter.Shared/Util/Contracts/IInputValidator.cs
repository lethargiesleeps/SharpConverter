using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Contracts;

public interface IInputValidator
{
    public bool ValidateDecimalInput(string input);
    public bool ValidateOctalInput(string input);
    public bool ValidateHexadecimalInput(string input);
    public bool ValidateBinaryInput(string input);
    public string GenerateErrorMessage(NSCErrorState errorState);
}