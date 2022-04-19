using SharpConverter.Shared.Util.Converters;

namespace SharpConverter.Debug.NSCTests;

public static class DecimalTo_Tests
{
    private static NumberSystemConverter _nsc = new NumberSystemConverter();
    public static void DecimalToBinary()
    {
        Console.WriteLine("==Decimal To Binary Tests==");
        Console.WriteLine("\n<==Whole numbers==>");
        //Numeric whole number input
        Console.WriteLine(_nsc.DecimalToBinary("15"));
        Console.WriteLine(" => Should be: 1111\n");
        Console.WriteLine(_nsc.DecimalToBinary("31"));
        Console.WriteLine(" => Should be: 11111\n");
        Console.WriteLine(_nsc.DecimalToBinary("666"));
        Console.WriteLine(" => Should be: 1010011010\n");
        Console.WriteLine(_nsc.DecimalToBinary("6969"));
        Console.WriteLine(" => Should be: 1101100111001\n");

        //Invalid inputs
        Console.WriteLine("\n<==Invalid Inputs==>");
        Console.WriteLine(_nsc.DecimalToBinary(""));
        Console.WriteLine(" => Should be: Value Is Empty\n");
        Console.WriteLine(_nsc.DecimalToBinary("     "));
        Console.WriteLine(" => Should be: Enter a decimal value. (Numbers containing 0-9 and/or decimal values.)\n");
        Console.WriteLine(_nsc.DecimalToBinary("-666"));
        Console.WriteLine(" => Should be: Try to enter positive values only. For accurate depiction of negative values, use the IEEE754 conversion tool.\n");
        Console.WriteLine(_nsc.DecimalToBinary("1543.123.45"));
        Console.WriteLine(" => Should be: Something about to many points.\n");
    }
}