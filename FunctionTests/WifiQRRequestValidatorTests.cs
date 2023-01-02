using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeveco.FunctionTests;
public class WifiQRRequestValidatorTests
{
    readonly WifiQRRequestValidator validator = new();

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public void FieldsEmpty_ReturnsValidationErrors(string wifiName, string passcode)
    {
        // Arrange
        WifiQRRequest wifiRequest = new()
        {
            WifiName = wifiName,
            Passcode = passcode
        };

        // Act
        var response = validator.TestValidate(wifiRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.WifiName);
        response.ShouldHaveValidationErrorFor(x => x.Passcode);
    }

    [Fact]
    public void PasscodeUnder16Chars_ReturnsValidationErrors()
    {
        // Arrange
        WifiQRRequest wifiRequest = new()
        {
            WifiName = new string('a', 1),
            Passcode = new string('a', 15)
        };
        // Act
        var response = validator.TestValidate(wifiRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.Passcode);
    }
    [Fact]
    public void PasscodeOver200Chars_ReturnsValidationErrors()
    {
        // Arrange
        WifiQRRequest wifiRequest = new()
        {
            WifiName = new string('a', 1),
            Passcode = new string('a', 201)
        };
        // Act
        var response = validator.TestValidate(wifiRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.Passcode);
    }
}