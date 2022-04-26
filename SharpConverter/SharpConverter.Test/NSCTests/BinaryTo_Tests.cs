using SharpConverter.Shared.Util.Converters;

namespace SharpConverter.Debug.NSCTests;

public static class BinaryTo_Tests
{
    private static readonly NumberSystemConverter _nsc = new();

    public static void BinaryToDecimal()
    {
        Console.WriteLine("Enter binary number");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.BinaryToDecimal(input));
    }

    public static void BinaryToOctal()
    {
        Console.WriteLine("Enter binary number:");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.BinaryToOctal(input));
    }

    public static void BinaryToHexadecimal()
    {
        Console.WriteLine("Enter binary number:");
        var input = Console.ReadLine();
        Console.WriteLine(_nsc.BinaryToHexadecimal(input));
    }
}