using SharpConverter.Debug.NSCTests;
using SharpConverter.Debug.UITests;

Console.BackgroundColor = ConsoleColor.Yellow;
Console.ForegroundColor = ConsoleColor.Black;
Console.Clear();

//TESTS
//DecimalTo_Tests.DecimalToBinary(); => Pass
//BinaryTo_Tests.BinaryToDecimal(); //=> Progress
StringValidation_Tests.PreConversion_Test();
Console.ReadKey();
