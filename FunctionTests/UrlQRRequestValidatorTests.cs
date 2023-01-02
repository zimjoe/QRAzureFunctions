namespace Aeveco.FunctionTests;

public class UrlQRRequestValidatorTests
{
    readonly UrlQRRequestValidator validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FieldsEmpty_ReturnsValidationErrors(string url)
    {
        // Arrange
        UrlQRRequest urlRequest = new ()
        {
            Url= url
        };

        // Act
        var response = validator.TestValidate(urlRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.Url);
    }

    // test valuse must also pass the minimum length valuation
    [Theory]
    [InlineData("http://TestUrl.com")]
    [InlineData("https://TestUrl.com")]
    public void UrlMustStartWith_ValidationSucceeds(string url)
    {
        // Arrange
        UrlQRRequest urlRequest = new()
        {
            Url = url
        };

        // Act
        var response = validator.TestValidate(urlRequest);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.Url);
    }

    [Fact]
    public void UrlIsUnder10Chars_ReturnsValidationErrors()
    {
        // Arrange
        UrlQRRequest urlRequest = new()
        {
            Url = new string('a', 9)
        };

        // Act
        var response = validator.TestValidate(urlRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.Url);
    }

    [Fact]
    public void UrlIsOver2048Chars_ReturnsValidationErrors()
    {
        // Arrange
        string val = $"https://{new string('a', 2049)}";
        UrlQRRequest urlRequest = new()
        {
            Url = val
        };

        // Act
        var response = validator.TestValidate(urlRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.Url);
    }
}