using SharpConverter.Shared.Util;

NumberSystemConverter numberSystemConverter = new();
MenuManager menuManager = new SharpConverter.Shared.Util.MenuManager(true, numberSystemConverter);

Console.WriteLine("Enter a decimal value.");
string input = Console.ReadLine();
Console.WriteLine(numberSystemConverter.DecimalToBinary(input));