using SharpConverter.Shared.Util.Converters;

namespace SharpConverter.Debug.NSCTests;

public static class DecimalTo_Tests
{
    private static NumberSystemConverter _nsc = new NumberSystemConverter();
    public static void DecimalToBinary()
    {
        Console.WriteLine("Enter decimal number.");
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
            Console.WriteLine(_nsc.DecimalToBinary(input));
        else
            throw new NullReferenceException("Test rendered null. Debug Project | DecimalTo_Tests.cs |");
        Console.ReadKey();
    }


    public static void DecimalToHexadecimal()
    {
        Console.WriteLine("Enter decimal number.");
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
            Console.WriteLine(_nsc.DecimalToHexadecimal(input));
        else
            throw new NullReferenceException("Test rendered null. Debug Project | DecimalTo_Tests.cs |");
        Console.ReadKey();
    }

    public static void DecimalToOctal()
    {
        Console.WriteLine("Enter decimal number.");
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
            Console.WriteLine(_nsc.DecimalToOctal(input));
        else
            throw new NullReferenceException("Test rendered null. Debug Project | DecimalTo_Tests.cs |");
        Console.ReadKey();
    }
}