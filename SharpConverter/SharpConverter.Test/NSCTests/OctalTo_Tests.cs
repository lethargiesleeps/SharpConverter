using SharpConverter.Shared.Util.Converters;

namespace SharpConverter.Debug.NSCTests;

public class OctalTo_Tests
{
    private static readonly NumberSystemConverter _nsc = new();

    public static void OctalToDecimal()
    {
        Console.WriteLine("Enter octal number:");
        var input = Console.ReadLine()!;
        Console.WriteLine(_nsc.OctalToDecimal(input));
    }

    public static void OctalToBinary()
    {
        Console.WriteLine("Enter octal number:");
        var input = Console.ReadLine()!;
        Console.WriteLine(_nsc.OctalToBinary(input));
    }

    public static void OctalToHexadecimal()
    {
        Console.WriteLine("Enter octal number:");
        var input = Console.ReadLine()!;
        Console.WriteLine(_nsc.OctalToHexadecimal(input));
    }
}