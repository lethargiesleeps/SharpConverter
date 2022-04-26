using SharpConverter.Shared.Util.Converters;

namespace SharpConverter.Debug.NSCTests;

public static class HexadecimalTo_Tests
{
    private static readonly NumberSystemConverter _nsc = new();

    public static void HexToDecimal()
    {
        Console.WriteLine("Enter hex value:");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.HexadecimalToDecimal(input!));
    }

    public static void HexToOctal()
    {
        Console.WriteLine("Enter hex value:");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.HexadecimalToOctal(input!));
    }

    public static void HexToBinary()
    {
        Console.WriteLine("Enter hex value:");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.HexadecimalToBinary(input!));
    }
}