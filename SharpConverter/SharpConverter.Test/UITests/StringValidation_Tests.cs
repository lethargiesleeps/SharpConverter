using SharpConverter.Shared.Util.Converters;

namespace SharpConverter.Debug.UITests;

public static class StringValidation_Tests
{
    private static NumberSystemConverter _nsc = new NumberSystemConverter();
    public static void DecimalInput_Test()
    {
        Console.WriteLine("Enter decimal value");
        string input = Console.ReadLine();
        Console.WriteLine(_nsc.ValidateDecimalInput(input));

    }

    public static void PreConversion_Test()
    {
        Console.WriteLine("Enter decimal value");
        string input = Console.ReadLine();
        Console.WriteLine(_nsc.DecimalToBinary(input));
    }

    public static void BinaryInput_Test()
    {
        Console.WriteLine("Enter binary value");
        string? input = Console.ReadLine();
        Console.WriteLine(_nsc.ValidateBinaryInput(input));
    }
}