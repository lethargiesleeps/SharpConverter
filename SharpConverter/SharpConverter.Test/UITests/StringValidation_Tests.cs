using SharpConverter.Shared.Util.Converters;

namespace SharpConverter.Debug.UITests;

public static class StringValidation_Tests
{
    private static readonly NumberSystemConverter _nsc = new();

    public static void DecimalInput_Test()
    {
        Console.WriteLine("Enter decimal value");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.ValidateDecimalInput(input!));
    }

    public static void BinaryInput_Test()
    {
        Console.WriteLine("Enter binary value");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.ValidateBinaryInput(input!));
    }

    public static void OctalInput_Test()
    {
        Console.WriteLine("Enter octal value:");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.ValidateOctalInput(input!));
    }

    public static void HexInput_Test()
    {
        Console.WriteLine("Enter hex value:");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.ValidateHexadecimalInput(input!));
    }
}