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
    public void PasscodeUnder12Chars_ReturnsValidationErrors()
    {
        // Arrange
        WifiQRRequest wifiRequest = new()
        {
            WifiName = new string('a', 5),
            Passcode = new string('a', 11)
        };
        // Act
        var response = validator.TestValidate(wifiRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.Passcode);
    }
    [Fact]
    public void PasscodeOver64Chars_ReturnsValidationErrors()
    {
        // Arrange
        WifiQRRequest wifiRequest = new()
        {
            WifiName = new string('a', 5),
            Passcode = new string('a', 65)
        };
        // Act
        var response = validator.TestValidate(wifiRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.Passcode);
    }

    [Fact]
    public void WifiNameUnder4Chars_ReturnsValidationErrors()
    {
        // Arrange
        WifiQRRequest wifiRequest = new()
        {
            WifiName = new string('a', 3),
            Passcode = new string('a', 15)
        };
        // Act
        var response = validator.TestValidate(wifiRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.WifiName);
    }
    [Fact]
    public void WifiNameOver64Chars_ReturnsValidationErrors()
    {
        // Arrange
        WifiQRRequest wifiRequest = new()
        {
            WifiName = new string('a', 65),
            Passcode = new string('a', 64)
        };
        // Act
        var response = validator.TestValidate(wifiRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.WifiName);
    }
}