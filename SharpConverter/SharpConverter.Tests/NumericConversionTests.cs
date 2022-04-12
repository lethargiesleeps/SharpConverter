using Xunit;
using SharpConverter.Shared.Util.Converters;
namespace SharpConverter.Tests;

public class NumericConversionTests
{
    private NumberSystemConverter _nsc = new NumberSystemConverter();
    [Fact]
    public void DecimalToBinary_WholeNumbersShouldConvert()
    {
        //Arrange
        string expectedConversion = "15 in binary is: \n1111";
        //Act
        string actualConversion = _nsc.DecimalToBinary("15");
        //Assert
        Assert.Equal(expectedConversion,actualConversion);
    }
}