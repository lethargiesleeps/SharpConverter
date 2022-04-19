using SharpConverter.Shared.Util.Converters;
namespace SharpConverter.Debug.NSCTests;

public static class BinaryTo_Tests
{
    private static NumberSystemConverter _nsc = new NumberSystemConverter();

    public static void BinaryToDecimal()
    {
        Console.WriteLine("Enter binary number");
        string input = Console.ReadLine();
        Console.WriteLine(_nsc.BinaryToDecimal(input));
    }

}