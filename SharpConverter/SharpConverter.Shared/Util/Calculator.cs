namespace SharpConverter.Shared.Util;

public static class Calculator
{
    public static string AddBinary(string topNumber, string bottomNumber)
    {
        var result = "";
        var sum = 0;
        int topIndex = topNumber.Length - 1, bottomIndex = bottomNumber.Length - 1;
        while (topIndex >= 0 || bottomIndex >= 0 || sum == 1)
        {
            sum += topIndex >= 0 ? topNumber[topIndex] - '0' : 0;
            sum += bottomIndex >= 0 ? bottomNumber[bottomIndex] - '0' : 0;
            result = (char) (sum % 2 + '0') + result;
            sum /= 2;
            topIndex--;
            bottomIndex--;
        }

        return result;
    }
}