using SharpConverter.Shared.Util.MenuManagement.StateMachines;

namespace SharpConverter.Shared.Util.Contracts;

public interface INSCInputValidator
{
    public string ValidateDecimalInput(string input);
    public string ValidateOctalInput(string input);
    public string ValidateHexadecimalInput(string input);
    public string ValidateBinaryInput(string input);

    public bool[] ValidationChecks();
}