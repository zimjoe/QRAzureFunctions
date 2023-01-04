namespace Aeveco.FunctionTests;

public class MailRequestValidatorTests
{
    readonly MailQRRequestValidator validator = new();

    [Theory]
    [InlineData(null, null, null, null)]
    [InlineData("", "", "", null)]
    [InlineData(" ", " ", " ", null)]
    public void FieldsEmpty_ReturnsValidationErrors(string mailReceiver, string subject, string message, int? encoding)
    {
        // Arrange
        MailQRRequest mailRequest = new()
        {
            MailReceiver = mailReceiver,
            Subject= subject,
            Message= message,
            Encoding = encoding
        };

        // Act
        var response = validator.TestValidate(mailRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.MailReceiver);
        response.ShouldNotHaveValidationErrorFor(x => x.Subject);
        response.ShouldNotHaveValidationErrorFor(x => x.Message);
        response.ShouldNotHaveValidationErrorFor(x => x.Encoding);
    }

    [Fact]
    public void ValuesTooLong_ReturnsValidationErrors()
    {
        // Arrange
        MailQRRequest mailRequest = new()
        {
            MailReceiver = new string('a', 201),
            Subject = new string('a', 101),
            Message = new string('a', 2001),
            Encoding = 4
        };
        // Act
        var response = validator.TestValidate(mailRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.MailReceiver);
        response.ShouldHaveValidationErrorFor(x => x.Subject);
        response.ShouldHaveValidationErrorFor(x => x.Message);
        response.ShouldHaveValidationErrorFor(x => x.Encoding);
    }

    // test email values
    [Theory]
    [InlineData("test@test.com")]
    [InlineData("test@test.co")]
    [InlineData("test@test.co.uk")]
    public void GoodEmail_ValidationSucceeds(string emailAddress)
    {
        // Arrange
        MailQRRequest mailRequest = new()
        {
            MailReceiver = emailAddress
        };

        // Act
        var response = validator.TestValidate(mailRequest);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.MailReceiver);
    }

    // test email values
    [Theory]
    [InlineData("test.com")]
    [InlineData("test")]
    [InlineData("@test.co.uk")]
    public void BadEmail_ReturnsValidationErrors(string emailAddress)
    {
        // Arrange
        MailQRRequest mailRequest = new()
        {
            MailReceiver = emailAddress
        };

        // Act
        var response = validator.TestValidate(mailRequest);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.MailReceiver);
    }
}

